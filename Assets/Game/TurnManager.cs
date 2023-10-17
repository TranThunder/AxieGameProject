using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Start is called before the first frame update
    int TeamTurn = 0;
    int EnemyTurn = 0;
    [SerializeField] float AttackMoveDistance;
    AxieManager axieManager;
    void Start()
    {
        axieManager = GetComponent<AxieManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    public void UpdateTurn()
    {
        if (TeamTurn == EnemyTurn)
        {
            axieManager.Slot[TeamTurn].position += new Vector3(3.1f, 0, 0);
            TeamTurn++;

        }
        else
        {
            axieManager.Slot[TeamTurn+5].position -= new Vector3(3.1f, 0, 0);
            EnemyTurn++;

            if (EnemyTurn == 6)
            {
                TeamTurn = 0;
                EnemyTurn = 0;
            }
        }
    }
}
