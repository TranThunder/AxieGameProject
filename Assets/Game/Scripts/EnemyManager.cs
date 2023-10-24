using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] ChimoraPlace;
    public GameObject[] WolfGang;
    public GameObject[] TretantfGang;
    public GameObject[] TBearMom;
    void Start()
    {
        int i = Random.Range(0, 3);
       
        switch(i)
        {
                case 0:
                {
                    for(int j=0;j< ChimoraPlace.Length;j++)
                    {
                        if(j<3)
                        {
                            Instantiate(WolfGang[0], ChimoraPlace[j].transform.position, Quaternion.identity, ChimoraPlace[j].transform);

                        }
                        else
                        {
                            Instantiate(WolfGang[1], ChimoraPlace[j].transform.position, Quaternion.identity, ChimoraPlace[j].transform);
                        }
                    }
                    break;
                }
                case 1:
                {

                    for (int j = 0; j < ChimoraPlace.Length; j++)
                    {
                        if (j < 3)
                        {
                            Instantiate(TretantfGang[0], ChimoraPlace[j].transform.position, Quaternion.identity, ChimoraPlace[j].transform);
                        }
                        else
                        {
                            if (j == 4)
                            {
                                Instantiate(TretantfGang[2], ChimoraPlace[j].transform.position, Quaternion.identity, ChimoraPlace[j].transform);
                            }
                            else Instantiate(TretantfGang[1], ChimoraPlace[j].transform.position, Quaternion.identity, ChimoraPlace[j].transform);
                        }
                    }
                    break;
                }
            case 2:
                {
                    for (int j = 0; j < ChimoraPlace.Length; j++)
                    {
                        if (j < 3)
                        {
                            Instantiate(TBearMom[0], ChimoraPlace[j].transform.position, Quaternion.identity, ChimoraPlace[j].transform);
                        }
                        else
                        {
                            Instantiate(TBearMom[1], ChimoraPlace[j].transform.position, Quaternion.identity, ChimoraPlace[j].transform);
                        }
                    }
                    break;
                }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
