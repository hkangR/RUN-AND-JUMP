using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : PlayerState
{
    public PlayerDoubleJump(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        player.jumpCount++;
        
        //player.canDoubleJump = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("double jump");

        //Debug.Log("DoubleJump");
        if(Input.GetKeyDown(GlobalManager.instance.keyMappings["Attack"]))//攻击状态
        {
            stateMachine.ChangeState(player.airAttack);
        }
        if(rb.velocity.y<0)
        {
            stateMachine.ChangeState(player.airState); 
        }
        if(xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
    }
}
