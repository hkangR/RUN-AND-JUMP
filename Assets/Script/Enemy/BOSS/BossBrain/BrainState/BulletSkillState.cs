using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkillState : EnemyState
{
    private Boss enemy;
    
    public BulletSkillState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        //播放动画
        base.Enter();
        enemy.isBusy = true;
        stateTimer = enemy.shootDuration;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;
    }

    public override void Update()
    {
        base.Update();
        //发射
        if (!enemy.isCreatingBullet)
        {
            enemy.StartCoroutine("CreatePhotonBullet");
            
        }
        if(stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}
