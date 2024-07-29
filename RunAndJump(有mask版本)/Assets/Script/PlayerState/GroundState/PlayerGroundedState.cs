using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Mouse0))//攻击状态
        {
            //Debug.Log("attack");
            stateMachine.ChangeState(player.primaryAttack);
        }

        if (!player.IsGroundDetected())//检测是否在地面上
            stateMachine.ChangeState(player.airState);
            
        if(Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())//地面可跳跃
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
