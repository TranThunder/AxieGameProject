﻿using Spine;
using Spine.Unity;
using System;
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
    public bool isatk;
    public bool idle=true;
    public float time;
    public bool myturn;
    SkeletonAnimation skeanimation;
    public TurnManager turnManager;
    Vector2 pos;
    // Start is called before the first frame update
    private void Awake()
    {
        
        pos = transform.position;
    }
    void Start()
    {
        turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
        skeanimation = GetComponent<SkeletonAnimation>();  
        skeanimation.AnimationState.Complete += AnimationRetreat;
        skeanimation.state.SetAnimation(0, "action/idle/normal", true);
    }

    // Update is called once per frame
    void Update()
    {

        if (isatk&&idle==false)
        {
            if(skeanimation.AnimationName!= "attack/melee/normal-attack")
            {
                skeanimation.state.SetAnimation(0, "attack/melee/normal-attack", false);
            }
            
            transform.position = Vector2.Lerp(transform.position, pos + new Vector2(4.2f, 0), 4*Time.deltaTime);
        }
        if(isatk==false && idle==false)
        {
            if (skeanimation.AnimationName != "action/move-back")
            {
                skeanimation.state.SetAnimation(0, "action/move-back", false);
            }
            transform.position = Vector2.Lerp(transform.position, pos, 4* Time.deltaTime);

        }
        if(idle)
        {
            if (skeanimation.AnimationName != "action/idle/normal")
            {
                skeanimation.state.SetAnimation(0, "action/idle/normal", true);
            }
        }
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
