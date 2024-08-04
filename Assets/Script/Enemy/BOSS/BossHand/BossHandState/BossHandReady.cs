using System.Collections;
using UnityEngine;

public class BossHandReady : EnemyState
{
    private BossHand enemy;
    private Player player;

    public BossHandReady(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, BossHand enemy) 
        : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        // 播放动画
        base.Enter();
        stateTimer = enemy.floatTime;
        player = PlayerManager.instance.player;
        enemy.transform.position += enemy.hitOffset; // 手掌悬浮位置
        
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
        // Debug.Log("Hit State");

        if (!enemy.isHitting)
        {
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, 
                new Vector3(player.transform.position.x,enemy.transform.position.y),  enemy.floatSpeed * Time.deltaTime);
        }

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.hitState);
        }

    }

   
}