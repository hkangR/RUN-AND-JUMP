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
        player.canDoubleJump = true;
    }

    public override void Update()
    {
        base.Update();
        
        if(player.IsGroundDetected())//地面检测
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.canDoubleJump && Input.GetKeyDown(KeyCode.Space))//二段跳
        {
            stateMachine.ChangeState(player.doubleJump);
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse0))//攻击状态
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
