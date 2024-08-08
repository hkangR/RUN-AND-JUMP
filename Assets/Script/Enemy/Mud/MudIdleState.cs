using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudIdleState : EnemyState
{
    private Enemy_Mud enemy;
    public MudIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Mud enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.sfxSource = enemy.GetComponent<AudioSource>();
        AudioManager.instance.PlaySFX("MudIdle");
        stateTimer = enemy.idleTime;
        
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.sfxSource.Stop();
    }

    public override void Update()
    {
        base.Update();
        
        enemy.SetVelocity(enemy.moveSpeed *enemy.facingDir,rb.velocity.y);
        if(stateTimer < 0)
        {
            enemy.Flip();
            stateTimer = enemy.idleTime;
        }

        if (enemy.IsPlayerDetected())
        {
            stateMachine.ChangeState(enemy.battleState);
        }
        
        if(enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            //enemy.SetVelocity(enemy.moveSpeed *enemy.facingDir,rb.velocity.y);
        }
        

    }
}
