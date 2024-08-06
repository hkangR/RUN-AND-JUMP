using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattyFallState : EnemyState
{
    private Enemy_Fatty enemy;
    private Player player;
    
    public FattyFallState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Fatty enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.floatTime;
        player = PlayerManager.instance.player;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (!enemy.isHitting)
        {
            //寻找玩家位置
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, 
                new Vector3(player.transform.position.x,enemy.transform.position.y),  enemy.jumpSpeed * Time.deltaTime);

            //enemy.transform.position = Vector3.Lerp(enemy.transform.position, player.transform.position, enemy.hitSpeed * Time.deltaTime);
            

            //enemy.StartCoroutine(enemy.Hit(player.transform.position));
        }
        
        if (stateTimer < 0f)
        {
            Debug.Log("Falling");
            enemy.isHitting = true;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, enemy.hitSpeed * Time.deltaTime);

            if (enemy.IsGroundDetected())
            {
                enemy.isHitting = false;
                stateMachine.ChangeState(enemy.idleState);
            }
        }
    }
}
