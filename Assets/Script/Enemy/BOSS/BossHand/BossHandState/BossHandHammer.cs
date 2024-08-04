using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandHammer : EnemyState
{
    private BossHand enemy;
    
    
    public BossHandHammer(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, BossHand enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        //播放动画
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
