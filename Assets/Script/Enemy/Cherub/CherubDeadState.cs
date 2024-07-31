using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherubDeadState : EnemyState
{
    private Enemy_Cherub enemy;
    
    public CherubDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Cherub enemy) : base(enemyBase, stateMachine, animBoolName)
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
    }

    public override void Update()
    {
        base.Update();
    }
}
