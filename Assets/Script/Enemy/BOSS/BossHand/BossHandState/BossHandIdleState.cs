using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandIdleState : EnemyState
{
    private BossHand enemy;
    
    
    public BossHandIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, BossHand enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        //播放动画
        base.Enter();//没素材我就不播了
        stateTimer = enemy.idleTime;//过度时间
        //enemy.isBusy = true;
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.isBusy = false;
    }

    public override void Update()
    {
        base.Update();
  
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.ready);
        }
    }
}
