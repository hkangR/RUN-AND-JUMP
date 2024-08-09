using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//要不要继承Enemy呢？
public class Boss : Enemy
{
    public bool isBusy = false;
    public bool isDead = false;
    public float bossDeathTime { get; private set; } = 2f;
    public bool isCreatingBullet { get; private set; } = false;
    [SerializeField] public float shootDuration;
    [SerializeField] private List<GameObject> photonBullet;
    [SerializeField] private Vector3 bulletOffset;
    
    [SerializeField] public bool isSecondStage;
    
    public BossIdleState idleState { get; private set; } //检测到玩家激活Boss,双手进入攻击状态，同时作为过度状态
    public BulletSkillState bulletSkillState { get; private set; } //弹幕攻击状态
    
    public BossDeathState deathState { get; private set; } 
    protected override void Awake()
    {
        base.Awake();
        //enemyProperty = GetComponent<EnemyProperty>();
        
        idleState = new BossIdleState(this, stateMachine, "Idle", this);
        deathState = new BossDeathState(this, stateMachine, "Die", this);
        bulletSkillState = new BulletSkillState(this, stateMachine, "BulletSkill", this);
        
    }
    
    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0f;//boss不受重力影响

        stateMachine.Initialize(idleState);
    }
    
    //自己能发射弹幕
    public IEnumerator CreatePhotonBullet()
    {
        isCreatingBullet = true;
        for (int i = 0; i < photonBullet.Count; i++)
        {
            GameObject obj1 = ObjectPool.instance.GetObject(photonBullet[i], transform.position, transform);
            GameObject obj2 = ObjectPool.instance.GetObject(photonBullet[i], transform.position + bulletOffset, transform);
            GameObject obj3 = ObjectPool.instance.GetObject(photonBullet[i], transform.position - bulletOffset, transform);
            obj1.GetComponent<BulletControl>().AimPlayer();
            obj2.GetComponent<BulletControl>().AimPlayer();
            obj3.GetComponent<BulletControl>().AimPlayer();
            yield return new WaitForSeconds(0.8f);
        }
        
        isCreatingBullet = false;
        stateMachine.ChangeState(idleState);
        
    }

    public override void Die()
    {
        base.Die();
        //进入二阶段
        if (!isSecondStage)
        {
            isSecondStage = true;
            enemyProperty.Revive(0.5f);
        }
        else
        { 
            stateMachine.ChangeState(deathState);
        }
    }
    
}
