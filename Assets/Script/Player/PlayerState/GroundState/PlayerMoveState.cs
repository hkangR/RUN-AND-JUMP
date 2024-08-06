using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.sfxSource = player.GetComponent<AudioSource>();
        AudioManager.instance.PlaySFX("Move");
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.sfxSource.Stop();

    }

    public override void Update()
    {
        base.Update();


        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (xInput == 0)
            stateMachine.ChangeState(player.idleState);
    }
}
