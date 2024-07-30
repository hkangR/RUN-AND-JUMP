using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormAttackState : EnemyState
{
    private Enemy_Worm enemy;

    public WormAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Worm enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(0,0);

        if(triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
