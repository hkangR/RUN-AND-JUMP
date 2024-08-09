using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Mask info")]
    [SerializeField] public GameObject mask;
    [SerializeField] public bool canCreateMask;
    
    [Header("Player info")]
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected float playerDetectedDistance;
    
    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    private float defaultMoveSpeed;//也是为减速留的
    
    [Header("Attack info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttack;
    
    [SerializeField] public Material material = null;
    [SerializeField] public Material originalMaterial = null;
    
    
    protected EnemyProperty enemyProperty;
    
    public EnemyStateMachine stateMachine { get; private set; }
    
    public string lastAnimBoolName { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        defaultMoveSpeed = moveSpeed;
        enemyProperty = GetComponent<EnemyProperty>();

        originalMaterial = GetComponentInChildren<Renderer>().material;
        material = originalMaterial;
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
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position,new Vector3(transform.position.x + playerDetectedDistance * facingDir,transform.position.y));
    }
    
    
    public override void Die() {
        base.Die();
    }

    public void CauseDamage(Player player) 
    {
        if(!player.canBeAttacked) return;
        float amount;
        if (enemyProperty) 
        {
            //Debug.Log(enemyProperty.atkResult);
            amount = enemyProperty.atkResult;
        }
        else 
        {
            return;
        }
        player.TakeDamage(amount);
    }
    public virtual void TakeDamage(float damage) 
    {
        enemyProperty.RemoveProperty(PropertyType.HPValue,damage);
        if (enemyProperty.hpValue <= 0) {
            Die();
        }
    }
}
