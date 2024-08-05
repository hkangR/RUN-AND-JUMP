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
        //enemy.isBusy = true;
        stateTimer = enemy.idleTime;//过度时间
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.isBusy = false;
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("idle");
        
        if (stateTimer < 0)
        {
            int randomSkill = Mathf.RoundToInt(Random.Range(0, 2));//随机选择攻击
            
            if(randomSkill <= 1)
                stateMachine.ChangeState(enemy.bulletSkillState);
            else
            {
                //应该是其他攻击方式
                stateMachine.ChangeState(enemy.idleState);
            }
                
        }
    }
}
