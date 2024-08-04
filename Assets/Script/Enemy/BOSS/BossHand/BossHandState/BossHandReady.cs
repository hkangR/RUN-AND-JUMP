using System.Collections;
using UnityEngine;

public class BossHandReady : EnemyState
{
    private BossHand enemy;

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
        enemy.transform.position += enemy.hitOffset; // 手掌悬浮位置
        //enemy.transform.position = Vector3.Lerp(enemy.transform.position, enemy.transform.position + enemy.hitOffset, 2* Time.deltaTime);
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
        // Debug.Log("Hit State");
        

        if (stateTimer < 0)
        {
            //stateMachine.ChangeState(enemy.hitState);
            stateMachine.ChangeState(enemy.hammerState);
        }

    }

   
}