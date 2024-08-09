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
        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!boss.isSecondStage && !boss.isBusy && !bossHands[0].isBusy)
        {
            int random = Random.Range(0, 2);
            switch (random)//选择一种攻击方式
            {
                case 0: 
                    boss.stateMachine.ChangeState((boss.bulletSkillState)); 
                    break;
                case 1:
                    foreach (var hand in bossHands)
                    {
                        hand.stateMachine.ChangeState(hand.ready);
                    }
                    break;
                default: break;
            }
        }
        //二阶段疯狂攻击
        else if(boss.isSecondStage)
        {
            if(!boss.isBusy)
                boss.stateMachine.ChangeState((boss.bulletSkillState));

            if (!bossHands[0].autoAttack)
            {
                foreach (var hand in bossHands)
                {
                    hand.autoAttack = true;
                }
            }

        }
    }
}
