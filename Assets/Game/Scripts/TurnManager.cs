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
    Vector3 distance = new Vector3(3.1f,0,0);

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
    void AxieAnimation()
    {
        
        axieManager.Slot[TeamTurn].position = Vector3.Slerp(axieManager.Slot[TeamTurn].position, axieManager.Slot[TeamTurn].position + distance, 0.5f);

    }
}
