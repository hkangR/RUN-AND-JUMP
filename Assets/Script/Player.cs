using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public bool isBusy { get; private set; }

    public bool canMakeMask;
    [SerializeField] public GameObject mask;
    
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;
    
    [Header("Move Info")]
    [SerializeField] public float moveSpeed = 8f;
    [SerializeField] public float jumpForce = 12f;
    private float defaultMoveSpeed;
    private float defaultJumpForce;
    
    [Header("Dash Info")]
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashDuration;
    [SerializeField] public float dashTimer;
    [SerializeField] private float dashCooldown;

    public float dashDir { get; private set; }
    
    private float defaultDashSpeed;
    
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerPrimaryAttack primaryAttack { get; private set; }
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        
        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
        
    }
    
    protected override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(idleState);

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

    }
    
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
    
    
    
}
