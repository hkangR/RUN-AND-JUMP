using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherubFlyState : EnemyState
{
    private Enemy_Cherub enemy;
    
    protected Transform player;
    
    public CherubFlyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Cherub enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.flyTime;
        enemy.SetVelocity(0,enemy.launchSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Debug.Log("Fly State");

        
        if (!enemy.CherubPlayerDetected())
        {
            stateMachine.ChangeState(enemy.fallState);
        }
        
        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(enemy.attackState);
        } 
        
        
    }
}
