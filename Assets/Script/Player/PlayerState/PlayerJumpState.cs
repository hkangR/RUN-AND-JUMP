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
        //player.canDoubleJump = true;
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

        //其实这里JumpCount一定会是1但是保险起见还是加个判断
        if (player.jumpCount == 1 && Input.GetKeyDown(InputManager.instance.keyMappings["Jump"]))//二段跳
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
