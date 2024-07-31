using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherubIdleState : EnemyState
{
    private Enemy_Cherub enemy;
    
    protected Transform player;
    
    public CherubIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Cherub enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        base.Enter();
        //player = PlayerManager.instance.player.transform;
        player = enemy.selfPlayer.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("Idle State");
        if(enemy.CherubPlayerDetected() || Vector2.Distance(enemy.transform.position,player.position)<2)
        {
            stateMachine.ChangeState(enemy.flyState);
        }

        if (!enemy.CherubPlayerDetected())
        {
            stateMachine.ChangeState(enemy.fallState);
        }
    }
    
}
