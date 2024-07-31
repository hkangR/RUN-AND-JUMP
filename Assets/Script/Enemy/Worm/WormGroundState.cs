using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormGroundState : EnemyState
{
    protected Enemy_Worm enemy;
    
    public WormGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Worm enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        //player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(enemy.IsPlayerDetected())
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
