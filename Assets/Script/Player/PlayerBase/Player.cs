using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Entity
{
    public bool isOnTheAir;
    public bool isBusy { get; private set; }
    public bool canDoubleJump;
    public bool canMakeMask;
    [SerializeField] public GameObject maskFX;
    [SerializeField] public GameObject mask;
    [SerializeField] public Transform attackTransform;
    [SerializeField] public Vector3 attackOffset = new Vector3(0.7f, 0, 0);
    
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;
    public float beAttackForce = 5f;
    public bool canBeAttacked = true;
    
    [Header("Move Info")]
    [SerializeField] public float moveSpeed = 8f;
    [SerializeField] public float jumpForce = 12f;
    private float defaultMoveSpeed;//为 减速/加速 buff留的
    private float defaultJumpForce;
    
    [Header("Dash Info")]
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashDuration;
    [SerializeField] public float dashTimer;
    [SerializeField] private float dashCooldown;

    public float dashDir { get; private set; }
    
    private float defaultDashSpeed;//也是为 减速/加速 buff留的
    
    private PlayerProperty playerProperty;
    private PlayerRespawn playerRespawn;

    public bool hasAb3 = false;
    
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDoubleJump doubleJump { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerDeathState deathState { get; private set; }
    public PlayerPrimaryAttack primaryAttack { get; private set; } 
    public PlayerAirAttack airAttack { get; private set; } 
    public PlayerGetHit getHitState { get;private set; }
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        doubleJump = new PlayerDoubleJump(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        
        dashState = new PlayerDashState(this, stateMachine, "Dash", "Slide");
        //slideState = new PlayerSlideState(this, stateMachine, "Slide");
        deathState = new PlayerDeathState(this, stateMachine, "Die");
        
        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
        airAttack = new PlayerAirAttack(this, stateMachine, "AirAttack");
        
        getHitState = new PlayerGetHit(this, stateMachine, "GetHit");
        playerProperty = GetComponent<PlayerProperty>();
        
    }
    
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

        playerRespawn = GetComponent<PlayerRespawn>();
        
        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;
        
    }
    
    protected override void Update()
    {
        
        base.Update();
        stateMachine.currentState.Update();
        
        dashTimer -= Time.deltaTime;

        CheckForDashInput();
        isOnTheAir = !IsGroundDetected();
    }
    
    //有时充当lock作用，主要在Attack中
    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(seconds);

        isBusy = false;
    }
    
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckForDashInput()
    {
        //dashUsageTimer -= Time.deltaTime;
        
        if (IsWallDetected()) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {
            dashTimer = dashCooldown;
                
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0) dashDir = facingDir;
            
            stateMachine.ChangeState(dashState);
        }
    }
    
    
    public override void Die() {
        stateMachine.ChangeState(deathState);
    }
    
    
    public void CauseDamage(Enemy enemy) 
    {
        //CameraManager.instance.virtualCamera.CameraShake();
        float amount;
        if (playerProperty) 
        {
            amount = playerProperty.getAtkResult();
        }
        else 
        {
            return;
        }
        enemy.TakeDamage(amount);
    }
    public void TakeDamage(float damage)
    {
        playerProperty.RemoveProperty(PropertyType.HPValue, damage);
        if (playerProperty.hpValue <= 0)
        {
            StartCoroutine("FlashFX");
            stateMachine.ChangeState(deathState);
        }
        else
        {
            /*StartCoroutine("FlashFX");
            //我们要保留一点Bug，这样玩家才能知道我们的游戏叫Defeat-Bug
            rb.velocity = new Vector2(rb.velocity.x, beAttackForce);//笑死，连续攻击能被打上天*/
            stateMachine.ChangeState(getHitState);
        }
    }

    //直接使用接触到的物件
    public void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == Tag.PICKABLE) 
        {
            PickableObject po = collision.gameObject.GetComponent<PickableObject>();
            if (po != null) 
            {
                UseItem(po.itemSO);
                po.Interact();
                //Destroy(po.gameObject);
            }
        }

        if (collision.gameObject.tag == Tag.CHIP)
        {
            PickableObject po = collision.gameObject.GetComponent<PickableObject>();
            if (po != null) 
            {
                UseItem(po.itemSO);
            }
            ChipUI.instance.PickChip(po.itemSO.prefab);
            Destroy(collision.gameObject);
        }
    }

    //使用物体
    public void UseItem(ItemSO itemSO) 
    {
        switch (itemSO.itemType) 
        {
            case ItemType.Consumable:
                playerProperty.UseItem(itemSO);
                break;
            case ItemType.Chip:
                playerProperty.UseItem(itemSO);
                break;
            default:
                break;
        }
    }

    public IEnumerator CreateMaskFX()
    {
        GameObject maskMat =Instantiate(maskFX, attackCheck.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);
        
        Destroy(maskMat);
    }
    
    public void onDead() {
        playerRespawn.Respawn();
    }
}
