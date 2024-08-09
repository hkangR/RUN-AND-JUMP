using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHand : Enemy
{
    private bool canAttack = true;
    public bool isBusy = false;
    public bool autoAttack = false;
    [SerializeField] private Boss bossBrain;
    [SerializeField] private float groundedTime;
    [SerializeField] private Vector3 originPos;//初始位置
    
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
        idleState = new BossHandIdleState(this, stateMachine, "RightIdle", this);
        ready = new BossHandReady(this, stateMachine, "RightReady", this);
        hitState = new BossHandHitState(this, stateMachine, "RightHit", this);
        hammerState = new BossHandHammer(this, stateMachine, "RightHammer", this);
        bossBrain = GameObject.Find("BossBrain").GetComponent<Boss>();
    }
    
    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0f;//boss不受重力影响
        originPos = transform.position;
        stateMachine.Initialize(idleState);
    }
    
    public IEnumerator Hit(Vector3 player, bool isHammer = false)
    {
        
        isHitting = true;
        DrawDamageArea();
        Vector3 targetPos = new Vector3(transform.position.x, player.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, hitSpeed * Time.deltaTime);//拍击
        
        if (Vector3.Distance(transform.position,targetPos) < 0.1f)
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

    //Hit用的逻辑伤害判定
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

    public override void TakeDamage(float damage)
    {
        bossBrain.TakeDamage(damage);//本体受伤
    }
}
