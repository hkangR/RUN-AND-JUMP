using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//要不要继承Enemy呢？
public class Boss : Enemy
{
    [SerializeField] public bool isCreatingBullet;
    [SerializeField] public List<GameObject> photonBullet;
    [SerializeField] private Vector3 bulletOffset;
    [SerializeField] private BossHand leftHand;
    [SerializeField] private BossHand rightHand;
    private EnemyProperty enemyProperty;
    
    public BossIdleState idleState { get; private set; } //检测到玩家激活Boss,双手进入攻击状态，同时作为过度状态
    public BulletSkillState bulletSkillState { get; private set; } //弹幕攻击状态
    protected override void Awake()
    {
        base.Awake();
        enemyProperty = GetComponent<EnemyProperty>();
        
        idleState = new BossIdleState(this, stateMachine, "Idle", this);
        bulletSkillState = new BulletSkillState(this, stateMachine, "BulletSkill", this);
        
    }
    
    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0f;//boss不受重力影响

        stateMachine.Initialize(idleState);
    }
    
    //位置始终保持在屏幕上方
    
    //左右手自由攻击
    
    //自己能发射弹幕
    public IEnumerator CreatePhotonBullet()
    {
        isCreatingBullet = true;
        for (int i = 0; i < photonBullet.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(photonBullet[i], transform.position, Quaternion.identity, transform);
            Instantiate(photonBullet[i], transform.position + bulletOffset,Quaternion.identity, transform);
            Instantiate(photonBullet[i], transform.position - bulletOffset,Quaternion.identity, transform);
        }
        
        isCreatingBullet = false;
        
    }
    
}
