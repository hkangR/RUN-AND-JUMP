using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerState
{
    public PlayerSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        player.canBeAttacked = false;
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
        player.canBeAttacked = true;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if(stateTimer <0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
