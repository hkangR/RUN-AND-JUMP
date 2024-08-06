using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandHitState : EnemyState
{
    private BossHand enemy;
    private Player player;
    
    public BossHandHitState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, BossHand enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        //播放动画
        base.Enter();
        stateTimer = enemy.shakeDuration;//动画播放时间
        player = PlayerManager.instance.player;
        enemy.isBusy = true;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;
    }

    public override void Update()
    {
        base.Update();
        
        
        //Debug.Log("hitState");
  
        if (stateTimer < 0)
        { 
            enemy.StartCoroutine(enemy.Hit());
        }
    }
}