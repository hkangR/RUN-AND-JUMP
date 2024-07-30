using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundState : EnemyState
{
    protected Enemy_Worm enemy;

    protected Transform player;
    public EnemyGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Worm enemy) : base(enemyBase, stateMachine, animBoolName)
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

        if(enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position,player.position)<2)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
