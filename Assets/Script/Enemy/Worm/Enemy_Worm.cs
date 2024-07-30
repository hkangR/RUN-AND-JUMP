using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Worm : Enemy
{
    [SerializeField] public bool canCreateMask;
    [SerializeField] public bool isGround;
    [SerializeField] public GameObject mask;
    [SerializeField] public float patrolTime;

    #region States
    public WormIdleState idleState { get; private set; }
    public WormMoveState moveState { get; private set; }
    public WormBattleState battleState { get; private set; }
    public WormAttackState attackState { get; private set; }
    public WormDeadState deadState { get; private set; }
    
    //public WormStunnedState stunnedState { get; private set; }
    #endregion
    
    protected override void Awake()
    {
        base.Awake();

        idleState = new WormIdleState(this, stateMachine, "Idle", this);
        moveState = new WormMoveState(this, stateMachine, "Move", this);
        battleState = new WormBattleState(this, stateMachine, "Move", this);//�������ȥ
        attackState = new WormAttackState(this, stateMachine, "Attack", this);
        deadState = new WormDeadState(this, stateMachine, "Dead", this);
        //stunnedState = new SkeletonStunnedState(this, stateMachine, "Stun", this);
    }
    
    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        isGround = IsGroundDetected();
    }

    public override void Die()
    {
        base.Die();

        Vector3 maskPos = transform.position;
        //stateMachine.ChangeState(deadState);
        if (canCreateMask)
        {
            Instantiate(mask, maskPos, Quaternion.identity);//原地生成mask
            Instantiate(mask, maskPos, Quaternion.identity);//原地生成mask
            Instantiate(mask, maskPos, Quaternion.identity);//原地生成mask
        }
        gameObject.SetActive(false);
    }
}
