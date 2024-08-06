using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.jumpCount++;
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("jump");

        if (player.jumpCount ==1 && Input.GetKeyDown(InputManager.instance.keyMappings["Attack"]))//二段跳
        {
            stateMachine.ChangeState(player.doubleJump);
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse0))//攻击状态
        {
            stateMachine.ChangeState(player.airAttack);
        }
        if(rb.velocity.y<0)
        {
            stateMachine.ChangeState(player.airState); 
        }
    }
}
