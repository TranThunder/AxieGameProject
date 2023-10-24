using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyStats : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    public float hp;
    public float atk;
    public float def;
    public bool isatk;
    public bool idle = true;
    Vector2 pos;
    public bool myturn;
    public TurnManager turnManager;
    SkeletonAnimation skeanimation;
    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        pos = transform.position;
        level = turnManager.round;
        hp = 150 + 45 * level;
        atk = 30 + 12 * level;
        def = 50 + 5 * level;
        skeanimation = GetComponent<SkeletonAnimation>();
        skeanimation.AnimationState.Complete += AnimationRetreat;
        skeanimation.state.SetAnimation(0, "action/idle/normal", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isatk && idle == false)
        {
            if (skeanimation.AnimationName != "attack/melee/normal-attack")
            {
                skeanimation.state.SetAnimation(0, "attack/melee/normal-attack", false);
            }

            transform.position = Vector2.Lerp(transform.position, pos - new Vector2(4.2f, 0), 4 * Time.deltaTime);
        }
        if (isatk == false && idle == false)
        {
            if (skeanimation.AnimationName != "action/move-back")
            {
                skeanimation.state.SetAnimation(0, "action/move-back", false);
            }
            transform.position = Vector2.Lerp(transform.position, pos,4 * Time.deltaTime);

        }
        if (idle)
        {
            if (skeanimation.AnimationName != "action/idle/normal")
            {
                skeanimation.state.SetAnimation(0, "action/idle/normal", true);
            }
        }
    }
    void AnimationRetreat(TrackEntry a)
    {
        if (a.Animation.Name == skeanimation.AnimationName)
        {
            switch (a.Animation.Name)
            {
                case "attack/melee/normal-attack":
                    {
                        isatk = false;
                        break;
                    }
                case "action/move-back":
                    {
                        idle = true;
                        if (myturn)
                        {
                            turnManager.UpdateTurn();
                            myturn = false;
                        }
                        break;

                    }
                case "action/idle/normal":
                    {
                        
                        break;
                    }
            }
        }

    }
}
