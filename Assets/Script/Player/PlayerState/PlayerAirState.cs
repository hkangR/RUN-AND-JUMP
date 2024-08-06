using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        //player.canDoubleJump = true;
        //Debug.Log("Can Double Jump.");
    }

    public override void Update()
    {
        base.Update();
        
        if(player.IsGroundDetected())//地面检测
        {
            player.jumpCount = 0;//落地了
            stateMachine.ChangeState(player.idleState);
        }

        if (Input.GetKeyDown(InputManager.instance.keyMappings["Jump"]))
        {
            if (player.jumpCount == 0)
            {
                stateMachine.ChangeState(player.jumpState);
            }
            else if (player.jumpCount == 1)//可以进行二段跳
            {
                stateMachine.ChangeState(player.doubleJump);//在里面jumpCount会变成2
            }
            
        }
        
        if(Input.GetKeyDown(InputManager.instance.keyMappings["Attack"]))//攻击状态
        {
            //Debug.Log("attack");
            stateMachine.ChangeState(player.airAttack);
        }
        
        if(xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
        
        /*if(player.IsWallDetected())//黏墙
        {
            stateMachine.ChangeState(player.wallSlideState);
        }*/
    }
}
