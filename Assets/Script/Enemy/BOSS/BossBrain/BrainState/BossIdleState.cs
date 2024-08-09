using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : EnemyState
{
    private Boss enemy;
    
    
    public BossIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Boss enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        //播放动画
        base.Enter();//没素材我就不播了
        enemy.isBusy = true;
        stateTimer = enemy.idleTime;//过度时间
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
            enemy.isBusy = false;
        
    }
}
