using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private string slide;
    private bool sliding;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName, string slide) : base(player, stateMachine, animBoolName)
    {
        this.slide = slide;
    }

    public override void Enter()
    {
        if (player.IsGroundDetected())
        {
            rb = player.rb;
            sliding = true;
            player.animator.SetBool(slide,true);
            triggerCalled = false;
        }
        else base.Enter();
        AudioManager.instance.sfxSource = player.GetComponent<AudioSource>();
        AudioManager.instance.PlaySFX("Dash");
        player.canDoubleJump = false;//冲刺完不能二段跳
        player.canBeAttacked = false;
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        if (sliding)
        {
            sliding = false;
            player.animator.SetBool(slide,false);
        }
        else base.Exit();
        AudioManager.instance.sfxSource.Stop();
        player.SetVelocity(0, rb.velocity.y);
        player.canBeAttacked = true;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        // if(stateTimer <0 && player.IsGroundDetected())
        if(stateTimer <0)
        {
            stateMachine.ChangeState(player.idleState);
        }

    }
}
