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
        player.jumpCount = 0;
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
        if(Input.GetKeyDown(InputManager.instance.keyMappings["Attack"]))//攻击状态
        {
            stateMachine.ChangeState(player.airAttack);
        }
        if(rb.velocity.y<0)
        {
            stateMachine.ChangeState(player.airState); 
        }
    }
}
