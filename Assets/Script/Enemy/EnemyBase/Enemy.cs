using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Player info")]
    [SerializeField] public GameObject selfPlayer;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] private float playerDetectedDistance;
    [SerializeField] public int beAttackNum;
    
    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    private float defaultMoveSpeed;
    
    [Header("Attack info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttack;
    
    public EnemyStateMachine stateMachine { get; private set; }
    
    public string lastAnimBoolName { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        defaultMoveSpeed = moveSpeed;
    }
    
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }
    
    public virtual void AssignLastAnimName(string animBoolName)
    {
        lastAnimBoolName = animBoolName;
    }
    
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerDetectedDistance, whatIsPlayer);
    
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
    
    
}
