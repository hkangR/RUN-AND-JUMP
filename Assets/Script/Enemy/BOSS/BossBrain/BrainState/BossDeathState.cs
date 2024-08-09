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
        base.Enter();//没素材我就不播了
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.isBusy = false;
    }

    public override void Update()
    {
        base.Update();
        
    }
}
