using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
    [SerializeField] public Boss boss;
    [SerializeField] public BossHand[] bossHands;


    private void Awake()
    {
        boss = GetComponentInChildren<Boss>();
        bossHands = GetComponentsInChildren<BossHand>();
        
        /*boss.stateMachine.Initialize(boss.idleState);
        foreach (BossHand hand in bossHands)
        {
            hand.stateMachine.Initialize(hand.idleState);
        }*/
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Debug.Log(!boss.isBusy);
        Debug.Log("hand:"+!bossHands[0].isBusy);
        if (!boss.isSecondStage && !boss.isBusy && !bossHands[0].isBusy)
        {
            int random = Random.Range(0, 2);
            Debug.Log(random);
            switch (random)//选择一种攻击方式
            {
                case 0: 
                    /*foreach (var hand in bossHands)
                    {
                        hand.stateMachine.ChangeState(hand.idleState);
                    }*/
                    boss.stateMachine.ChangeState((boss.bulletSkillState)); 
                    break;
                case 1:
                    //boss.stateMachine.ChangeState(boss.idleState);
                    foreach (var hand in bossHands)
                    {
                        hand.stateMachine.ChangeState(hand.ready);
                    }
                    break;
                default: break;
            }
        }
        //二阶段疯狂攻击
        else
        {
            boss.stateMachine.ChangeState((boss.bulletSkillState));
            foreach (var hand in bossHands)
            {
                hand.stateMachine.ChangeState(hand.ready);
            }
        }
    }
}
