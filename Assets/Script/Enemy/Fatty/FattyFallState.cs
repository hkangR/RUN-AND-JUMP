using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattyFallState : EnemyState
{
    Vector3 targetPos=new Vector3();
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
        player = GlobalManager.instance.player;

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
            // 预测玩家位置，假设玩家将继续以当前速度移动一小段时间
            Vector2 playerVector = new Vector2(player.rb.velocity.x * 0.3f, player.rb.velocity.y * 0.3f);
            Vector3 predictedPlayerPosition = player.transform.position +new Vector3(playerVector.x,playerVector.y);

            // 使用 Lerp 在跳跃时平滑移动到预测的目标位置的 X 和 Y 坐标
            Vector3 targetXZ = new Vector3(predictedPlayerPosition.x, enemy.transform.position.y, predictedPlayerPosition.z);
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, targetXZ, enemy.jumpSpeed * Time.deltaTime);
            
            /*//寻找玩家位置
            targetPos = player.transform.position;
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, 
                new Vector3(targetPos.x,enemy.transform.position.y),  enemy.jumpSpeed * Time.deltaTime);*/
            
        }
        //Debug.Log(targetPos);
        if (stateTimer < 0f)
        {
            Debug.Log("Falling");
            enemy.isHitting = true;
            // 确保目标位置不受玩家位置变化影响
            targetPos = new Vector3(enemy.transform.position.x, player.transform.position.y, enemy.transform.position.z);
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, targetPos, enemy.hitSpeed * Time.deltaTime);


            if (Vector3.Distance(enemy.transform.position,targetPos) < 0.1f)
            {
                AudioManager.instance.sfxSource = enemy.GetComponent<AudioSource>();
                AudioManager.instance.PlaySFX("Earthquake");
                CameraManager.instance.virtualCamera.CameraShake(1.5f,1f,0.5f);
                enemy.ShakeDamageArea();
                enemy.isHitting = false;
                stateMachine.ChangeState(enemy.idleState);
            }
        }
    }
}
