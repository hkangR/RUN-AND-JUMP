using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fatty : Enemy
{
    [SerializeField] public float hitSpeed;
    [SerializeField] public bool isHitting;
    [SerializeField] public float jumpSpeed;
    [SerializeField] public float jumpDistance;
    [SerializeField] public float floatTime;
    
    #region States
    public FattyIdleState idleState { get; private set; }
    //public MudRushState moveState { get; private set; }
    public FattyJumpState jumpState { get; private set; }//found player and rush to attack
    public FattyFallState fallState { get; private set; }
    public FattyDeadState deadState { get; private set; }
    
    //public WormStunnedState stunnedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new FattyIdleState(this, stateMachine, "Idle", this);
        jumpState = new FattyJumpState(this, stateMachine, "Jump", this);
        fallState = new FattyFallState(this, stateMachine, "Fall", this);
        deadState = new FattyDeadState(this, stateMachine, "Die", this);
    }
    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0f;
        stateMachine.Initialize(idleState);
    }
    
    
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, playerDetectedDistance);
    }

    public bool FattyPlayerDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position, playerDetectedDistance, whatIsPlayer);
    
        // 检查是否在圆形区域内检测到了玩家
        foreach (Collider2D collider in colliders)
        {
            // 如果检测到了玩家，返回 true
            if (collider.CompareTag("Player")) 
            {
                return true;
            }
        }
        // 如果没有检测到玩家，返回 false
        return false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Debug.Log("found player");
            CauseDamage(other.GetComponent<Player>());
        }
    }
    
}
