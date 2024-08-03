using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandHitState : EnemyState
{
    private BossHand enemy;
    private Player player;
    private int shakeDir = 1;//向右震动
    public BossHandHitState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, BossHand enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        //播放动画
        base.Enter();//没素材我就不播了
        stateTimer = enemy.floatTime;
        player = PlayerManager.instance.player;
        enemy.transform.position += enemy.hitOffset;//手掌悬浮位置
        
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
       //Debug.Log("Hit State");
        if (!enemy.isHitting)
        {
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, 
                new Vector3(player.transform.position.x,enemy.transform.position.y),  enemy.floatSpeed * Time.deltaTime);
        }
        
        //Debug.Log("idle");
        if (stateTimer < 0)
        { 
            enemy.StartCoroutine(enemy.Hit());
        }
    }
    
    
    
}
