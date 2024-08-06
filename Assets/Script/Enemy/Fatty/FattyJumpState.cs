using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattyJumpState : EnemyState
{
    private Enemy_Fatty enemy;
    private Player player;
    
    
    public FattyJumpState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Fatty enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player;
        stateTimer = 2f;
        AudioManager.instance.sfxSource = enemy.GetComponent<AudioSource>();
        AudioManager.instance.PlaySFX("FattyJump");
        //跳到合适的高度
        Vector3 enemyPos = enemy.transform.position;
        Vector3 targetPos = new Vector3(enemyPos.x, enemyPos.y + enemy.jumpDistance, enemyPos.z);
        while(Vector3.Distance(enemy.transform.position,targetPos) > 0.1f)
        {
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, targetPos, enemy.jumpSpeed * Time.deltaTime);
        }
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.sfxSource.Stop();
    }

    public override void Update()
    {
        base.Update();
        
        if (!enemy.isHitting)
        {
            //寻找玩家位置
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, 
                new Vector3(player.transform.position.x,enemy.transform.position.y),  enemy.jumpSpeed * Time.deltaTime);

            if (stateTimer < 0f)
            {
                enemy.stateMachine.ChangeState(enemy.fallState);
            }
        }
    }
}
