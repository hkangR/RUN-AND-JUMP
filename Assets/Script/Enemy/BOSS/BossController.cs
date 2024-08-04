using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] public Boss boss;
    [SerializeField] public BossHand[] bossHands;


    private void Awake()
    {
        boss = GetComponentInChildren<Boss>();
        bossHands = GetComponentsInChildren<BossHand>();
    }
    
    
}
