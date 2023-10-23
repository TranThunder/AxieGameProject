using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxieStats : MonoBehaviour
{
    public int level;
    public float hp;
    public float atk;
    public float def;
    public float crit;
    public bool isatk=true;
    SkeletonAnimation animation;
    Vector2 pos;
    // Start is called before the first frame update
    private void Awake()
    {
        pos = transform.position;
    }
    void Start()
    {
        animation = GetComponent<SkeletonAnimation>();  
        animation.AnimationState.Complete += AnimationRetreat;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isatk)
        {
            
            transform.position = Vector2.Lerp(transform.position, pos + new Vector2(3.1f,0), 1.25f * Time.deltaTime) ;
        }
        else transform.position = Vector2.Lerp(transform.position,pos,1.25f* Time.deltaTime);
       

    }
    public void Stats(int lv)
    {
        level = lv;
        hp = 300 + lv * 10;
        atk = 30 + lv * 3;
        crit = 40;
        def = 50 + lv * 5;
    }
    void AnimationRetreat(TrackEntry a)
    {
        Debug.Log("ashdgjad" + a.Animation.Name);
        switch (a.Animation.Name)
        {
            case "action/action/move-backward":
                {
                    
                    break;
                }
        }
        
    }
}
