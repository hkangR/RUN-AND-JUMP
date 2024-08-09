using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : EnemyState
{
    private Boss enemy;
    
    
    public BossDeathState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        //播放动画
        base.Enter();
        enemy.isBusy = true;
        stateTimer = enemy.bossDeathTime;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            enemy.isDead = true;
        }
    }
}
