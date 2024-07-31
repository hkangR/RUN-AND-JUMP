using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudAttackState : EnemyState
{
    private Enemy_Mud enemy;

    public MudAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Mud enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        //Debug.Log("dash");
        base.Enter();
        stateTimer = enemy.dashTime;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttack = Time.time;
        
    }

    public override void Update()
    {
        base.Update();
        
        enemy.SetVelocity(enemy.dashSpeed * enemy.facingDir, rb.velocity.y);
        //enemy.SetVelocity(0,0);

        if(stateTimer < 0)
        {
            enemy.SetVelocity(0,0);
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
