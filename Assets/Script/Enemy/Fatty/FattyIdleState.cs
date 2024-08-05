using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattyIdleState : EnemyState
{
    private Enemy_Fatty enemy;
    
    
    public FattyIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Fatty enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("Idle State");
        if(enemy.FattyPlayerDetected() && stateTimer < 0f)
        {
            stateMachine.ChangeState(enemy.jumpState);
        }
        
    }
}
