using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattyDeadState : EnemyState
{
    private Enemy_Fatty enemy;
    
    
    public FattyDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Fatty enemy) : base(enemyBase, stateMachine, animBoolName)
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
