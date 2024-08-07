using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandHammer : EnemyState
{
    private BossHand enemy;
    private Player player;
    private bool groundCanShake;
    
    
    public BossHandHammer(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, BossHand enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        //播放动画
        base.Enter();
        player = GlobalManager.instance.player;
        stateTimer = enemy.hammerAnimationTime;
        groundCanShake = true;
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
        
        
        if (stateTimer < 0)
        {
            enemy.StartCoroutine(enemy.Hit(player.transform.position,true));
        }
        if (enemy.IsGroundDetected() && groundCanShake)
        {
            groundCanShake = false;
            CameraManager.instance.virtualCamera.CameraShake(2,2,0.5f);
        }
    }
    
    
}
