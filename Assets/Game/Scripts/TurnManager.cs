using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int TeamTurn = 0;
    public int EnemyTurn = 0;
    public int round=1;
    public Transform[] Slot;
    EnemyManager enemyManager;
    public AxieStats[] axie;


    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    public void UpdateTurn()
    {
       
        if (TeamTurn == EnemyTurn)
        {
            
            TeamTurn++;
            int j = 0;
            for(int i = 0;i<enemyManager.ChimoraPlace.Length;i++)
            {
               
                if (enemyManager.ChimoraPlace[i].GetComponentInChildren<EnemyStats>()!=null)
                {
                   
                    
                    if(j==EnemyTurn)
                    {
                        enemyManager.ChimoraPlace[j].GetComponentInChildren<EnemyStats>().myturn = true;
                        enemyManager.ChimoraPlace[j].GetComponentInChildren<EnemyStats>().isatk = true;
                        enemyManager.ChimoraPlace[j].GetComponentInChildren<EnemyStats>().idle= false;
                        
                        break;
                        
                    }
                    j++;
                }
            }

        }
        else
        {
            
            if (TeamTurn < 3) 
            {
                axie[TeamTurn].isatk = true;
                axie[TeamTurn].idle = false;
                axie[TeamTurn].myturn = true;
                EnemyTurn++;
            }
            else
            {
                EnemyTurn++;
                Debug.Log(EnemyTurn);

                if(EnemyTurn==6)
                {
                    TeamTurn = 0;
                    EnemyTurn = 0;
                    axie[TeamTurn].isatk = true;
                    axie[TeamTurn].idle = false;
                    axie[TeamTurn].myturn = true;
                }
                else UpdateTurn();

            }
            
           
        }
    }
    public void GetAxieTurn()
    {
        int j = 0;
        for(int i=0;i<Slot.Length;i++)
        {
            if(Slot[i].GetComponentInChildren<AxieStats>()!=null)
            {
                axie[j] = Slot[i].GetComponentInChildren<AxieStats>();
                j++;
            }
        }
        axie[0].isatk = true;
        axie[0].idle = false;
        axie[0].myturn = true;
    }
}
   
