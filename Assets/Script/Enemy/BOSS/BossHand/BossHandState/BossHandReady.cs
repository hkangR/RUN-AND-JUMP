using System.Collections;
using UnityEngine;

public class BossHandReady : EnemyState
{
    private int randomSkill;
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
        Debug.Log("enter");
        enemy.isBusy = true;
        stateTimer = enemy.floatTime;
        enemy.transform.position += enemy.hitOffset; // 手掌悬浮位置
        //enemy.transform.position = Vector3.Lerp(enemy.transform.position, enemy.transform.position + enemy.hitOffset, 2* Time.deltaTime);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;

    }

    public override void Update()
    {
        base.Update();
        // Debug.Log("Hit State");
        Debug.Log("isReady");

        if (stateTimer < 0)
        {
            if (randomSkill == 0)
            {
                randomSkill++;
                stateMachine.ChangeState(enemy.hitState);
            }
            else
            {
                randomSkill--;
                stateMachine.ChangeState(enemy.hammerState);
            }
            
        }

    }

   
}