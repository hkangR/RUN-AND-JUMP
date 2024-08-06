using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;

    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;
    
    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    
    public virtual void Enter()
    {
        rb = player.rb;
        player.animator.SetBool(animBoolName,true);
        triggerCalled = false;
    }
    
    public virtual void Update()
    {

        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw(InputManager.instance.axisMappings["Horizontal"]);
        yInput = Input.GetAxisRaw(InputManager.instance.axisMappings["Vertical"]);
        //xInput = Input.GetAxisRaw("Horizontal");
        //yInput = Input.GetAxisRaw("Vertical");

        player.animator.SetFloat("yVelocity", rb.velocity.y);
        
    }
    
    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName,false);
    }
    
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;

    }
    
}
