using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherubFallState : EnemyState
{
    private Enemy_Cherub enemy;
    
    protected Transform player;
    
    public CherubFallState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Cherub enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocity(0,-enemy.launchSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Debug.Log("Fall State");

        
        if (enemy.IsGroundDetected())
        {
            enemy.SetVelocity(0,0);
            enemy.stateMachine.ChangeState(enemy.idleState);
        }

    }
}
