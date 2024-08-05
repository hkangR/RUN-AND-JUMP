using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHand : Enemy
{
    private bool canAttack = true;
    public bool isBusy = false;
    [SerializeField] private float groundedTime;
    [SerializeField] private Vector3 originPos;//初始位置
    
    [SerializeField] public float yOffset;
    [SerializeField] public bool isHitting;
    [SerializeField] public Vector3 hitOffset;
    [SerializeField] private float hitSpeed;
    
    [SerializeField] public float floatSpeed;//左右浮动速度
    [SerializeField] public float floatTime;//左右浮动时间
    public float shakeDuration = 0.5f; // 摇晃持续时间
    
    [SerializeField] public float hammerAnimationTime;
    
    #region States
    public BossHandIdleState idleState { get;private set; }//出场和过度状态
    public BossHandReady ready { get; private set; }
    public BossHandHitState hitState { get; private set; }
    public BossHandHammer hammerState { get; private set; }
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        //enemyProperty = GetComponent<EnemyProperty>();
        idleState = new BossHandIdleState(this, stateMachine, "RightIdle", this);
        ready = new BossHandReady(this, stateMachine, "RightReady", this);
        hitState = new BossHandHitState(this, stateMachine, "RightHit", this);
        hammerState = new BossHandHammer(this, stateMachine, "RightHammer", this);
    }
    
    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0f;//boss不受重力影响
        originPos = transform.position;
        stateMachine.Initialize(idleState);
    }
    
    public IEnumerator Hit(bool isHammer = false)
    {
        
        isHitting = true;
        DrawDamageArea();
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0f-yOffset), hitSpeed * Time.deltaTime);//拍击
        
        if (IsGroundDetected())
        {
            canAttack = false;
            if(isHammer)
                HammerPlayer();
        }
        yield return new WaitForSeconds(groundedTime);
        
        
        isHitting = false;
        stateMachine.ChangeState(idleState);
        transform.position = Vector3.Lerp(transform.position, originPos,  hitSpeed * Time.deltaTime);//回到原位
        canAttack = true;
    }

    //Hit用的逻辑伤害判定，我也不知道为什么OnTriggerEnter触发不了
    private void DrawDamageArea()
    {
        if(!canAttack) return;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(3, 4f), 0f);
        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Player>() != null)
            {
                if (canAttack)
                {
                    CauseDamage(hit.GetComponent<Player>());
                    canAttack = false;//保证只击打一次
                    break;
                }
            }
            
        }
    }
    
    //Hmammer用的伤害判定，主打一个烫脚
    private void HammerPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(100f, 0.8f), 0f);

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Player>() != null)
            {
                CauseDamage(hit.GetComponent<Player>());
            }
            
        }
    }
    
}
