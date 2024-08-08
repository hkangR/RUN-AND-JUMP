using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudBattleState : EnemyState
{
    private Transform player;
    private Enemy_Mud enemy;

    private int moveDir;
    
    public MudBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Mud enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.sfxSource = enemy.GetComponent<AudioSource>();
        AudioManager.instance.PlaySFX("MudIdle");
        player = GlobalManager.instance.player.transform;
        enemy.StartCoroutine(WaitForRush(0.5f));
    }

    private IEnumerator WaitForRush(float time)
    {
        yield return new WaitForSeconds(time);
        
        enemy.SetVelocity(enemy.rushSpeed, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.sfxSource.Stop();
    }

    public override void Update()
    {
        base.Update();
        
        if(enemy.IsPlayerDetected())
        {
            
            stateTimer = enemy.battleTime;
            if(enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if(CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
              
            }
            
        }
        else
        {
            if(stateTimer < 0 || Vector2.Distance(player.transform.position,enemy.transform.position) > enemy.trackDistance)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        if(player.position.x > enemy.transform.position.x)
        {
            moveDir = 1; 
        }   
        else if(player.position.x <enemy.transform .position.x)
        {
            moveDir = -1;
        }
        
        enemy.SetVelocity(enemy.rushSpeed * moveDir, rb.velocity.y);
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttack + enemy.attackCooldown)
        {
            enemy.lastTimeAttack = Time.time;
            return true;
        }
        return false;
    }
}
