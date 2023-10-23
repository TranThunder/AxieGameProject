using AxieMixer.Unity;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AxieManager : MonoBehaviour
{
    Axie2dBuilder builder => Mixer.Builder;

    const bool USE_GRAPHIC = false;
    bool isFetchingGenes = false;
    public Transform[] Slot;
    public Text[] AxieInput;
    public string[] AxieSaveID; 
    int currentslot = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        Mixer.Init();
        List<string> animationList = builder.axieMixerMaterials.GetMixerStuff(AxieFormType.Normal).GetAnimatioNames();
        GetAxieOnStart();
        
    }

    // Update is called once per frame
    
    void Update()
    {
        
    }
    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void GetAxieOnStart()
    {
        int i = 0;
        foreach (string axieid in AxieSaveID)
        {
            
            AxieSaveID[i] = PlayerPrefs.GetString($"Axie{i}");
            if(AxieSaveID[i]=="")
            {
                AxieSaveID[i]= (222+i*111).ToString();
                StartCoroutine(GetAxiesGenes(AxieSaveID[i]));
            }
            else StartCoroutine(GetAxiesGenes(AxieSaveID[i]));
            i++;
        }
    }
    public void OnMixButtonClicked(int id)
    {

        if (isFetchingGenes) return;

        currentslot = id;
        PlayerPrefs.SetString($"Axie{id}", AxieInput[id].text);
        AxieSaveID[id] = AxieInput[id].text;
        StartCoroutine(GetAxiesGenes(AxieInput[id].text));
        
       
        
        
    }

    public IEnumerator GetAxiesGenes(string axieId)
    {
        Debug.Log("Ok");
        isFetchingGenes = true;
        string searchString = "{ axie (axieId: \"" + axieId + "\") { id, genes, newGenes}}";
        JObject jPayload = new JObject();
        jPayload.Add(new JProperty("query", searchString));

        var wr = new UnityWebRequest("https://graphql-gateway.axieinfinity.com/graphql", "POST");
        //var wr = new UnityWebRequest("https://testnet-graphql.skymavis.one/graphql", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jPayload.ToString().ToCharArray());
        wr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        wr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        wr.SetRequestHeader("Content-Type", "application/json");
        wr.timeout = 10;
        yield return wr.SendWebRequest();
        if (wr.error == null)
        {
            var result = wr.downloadHandler != null ? wr.downloadHandler.text : null;
            if (!string.IsNullOrEmpty(result))
            {
                JObject jResult = JObject.Parse(result);
                string genesStr = (string)jResult["data"]["axie"]["newGenes"];
                Debug.Log(genesStr);
                ProcessMixer(axieId, genesStr, USE_GRAPHIC);
            }
        }
        isFetchingGenes = false;
        
    }
    void ProcessMixer(string axieId, string genesStr, bool isGraphic)
    {
        int i = 0;
        if (string.IsNullOrEmpty(genesStr))
        {
            Debug.LogError($"[{axieId}] genes not found!!!");
            return;
            
            
        }
        float scale = 0.007f;

        var meta = new Dictionary<string, string>();
        //foreach (var accessorySlot in ACCESSORY_SLOTS)
        //{
        //    meta.Add(accessorySlot, $"{accessorySlot}1{System.Char.ConvertFromUtf32((int)('a') + accessoryIdx - 1)}");
        //}
        foreach (string axieid in AxieSaveID)
        {
            if (axieid ==axieId)
            {
                currentslot = i;
            }
            else i++;
        }
        var builderResult = builder.BuildSpineFromGene(axieId, genesStr, meta, scale, isGraphic);
    
            SpawnSkeletonAnimation(builderResult);
        
    }
    
    void SpawnSkeletonAnimation(Axie2dBuilderResult builderResult)
    {
        ClearAll();
        
        
        SkeletonAnimation runtimeSkeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(builderResult.skeletonDataAsset);
        
        runtimeSkeletonAnimation.transform.SetParent(Slot[currentslot].transform, false);
        runtimeSkeletonAnimation.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f); 
        runtimeSkeletonAnimation.gameObject.AddComponent<AutoBlendAnimController>();
        runtimeSkeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);
        runtimeSkeletonAnimation.tag=currentslot.ToString();
        if (builderResult.adultCombo.ContainsKey("body") &&
            builderResult.adultCombo["body"].Contains("mystic") &&
            builderResult.adultCombo.TryGetValue("body-class", out var bodyClass) &&
            builderResult.adultCombo.TryGetValue("body-id", out var bodyId))
        {
            runtimeSkeletonAnimation.gameObject.AddComponent<MysticIdController>().Init(bodyClass, bodyId);
        }
        runtimeSkeletonAnimation.skeleton.FindSlot("shadow").Attachment = null;
        runtimeSkeletonAnimation.loop=true;

       
    }
    void ClearAll()
    {
        var skeletonAnimations = FindObjectsOfType<SkeletonAnimation>();
        foreach (var p in skeletonAnimations)
        {
            if (p.tag == $"{currentslot}")
            {
                Destroy(p.gameObject);
            }
        }
        
       
    }
}
