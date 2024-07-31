using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherubAttackState : EnemyState
{
    private Enemy_Cherub enemy;
    
    protected Transform player;
    
    public CherubAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Cherub enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        base.Enter();
        enemy.rb.velocity = new Vector2(0, 0);
        stateTimer = enemy.battleTime;
        player = enemy.selfPlayer.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //Debug.Log("Attack State");
        if(!enemy.isCreatingBullet)
            enemy.StartCoroutine("CreatePhotonBullet");
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.fallState);
        }
    }
    
    
}
