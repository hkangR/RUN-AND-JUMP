using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
     private Boss boss;
     private BossHand[] bossHands;
     private Material bossMat;
     private Material bossLeftHandMat;
     private Material bossRightHandMat;
     //[SerializeField] private GameObject bossRoomMask;//boss死亡时生成
     [SerializeField] private float showTime;
     [SerializeField] private GameObject bossHealthBar;

    private void Awake()
    {
        boss = GetComponentInChildren<Boss>();
        bossHands = GetComponentsInChildren<BossHand>();
        bossMat = GetComponentInChildren<Renderer>().material;
        bossLeftHandMat = bossHands[0].GetComponentInChildren<Renderer>().material;
        bossRightHandMat = bossHands[1].GetComponentInChildren<Renderer>().material;
    }
    
    private void OnEnable()
    {
        StartCoroutine(FadeOutAndScale1());
    }
    
    private IEnumerator FadeOutAndScale1()
    {
        float elapsedTime = 0f;
        while (elapsedTime < showTime)
        {
            float progress = elapsedTime / showTime;

            // ScaleFactor from 6 to 0.01
            float newScale = Mathf.Lerp(6f, 0.01f, progress);
            bossMat.SetFloat("_Scale", newScale);
            bossLeftHandMat.SetFloat("_Scale", newScale);
            bossRightHandMat.SetFloat("_Scale", newScale);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return 1.5f;
        
        elapsedTime = 0f;
        while (elapsedTime < showTime)
        {
            float progress = elapsedTime / showTime;

            // Alpha blending from 1 to 0
            float newAlpha = Mathf.Lerp(1f, 0f, progress);
            bossMat.SetFloat("_Alpha", newAlpha);
            bossLeftHandMat.SetFloat("_Alpha", newAlpha);
            bossRightHandMat.SetFloat("_Alpha", newAlpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保透明度和缩放完全设置到最终值
        bossMat.SetFloat("_Alpha", 0f);
        bossMat.SetFloat("_Scale", 0.01f);
        bossHealthBar.SetActive(true);
        showTime = 0f;//演出结束

    }
    
    private void Update()
    {
        showTime -= Time.deltaTime;
        if (showTime <= 0)
        {
            BossAttackControl();
        }

        if (boss.isDead)
        {
            foreach (var hand in bossHands)
            {
                //boss死亡后，隐藏手
                hand.gameObject.SetActive(false);
            }
        }
    }

    private void BossAttackControl()
    {
        if(!boss.isSecondStage && !boss.isBusy && !bossHands[0].isBusy)
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
            if (boss.isDead)
            {
                //Instantiate(bossRoomMask,transform.position, Quaternion.identity);
                GlobalManager.instance.victorymenuController.Show();
                return;
            }
            
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
