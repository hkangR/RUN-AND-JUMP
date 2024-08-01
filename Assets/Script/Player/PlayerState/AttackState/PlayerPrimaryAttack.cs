using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;//连击次数

    private float lastTimeAttacked;
    private float comboWindow = 2;//两秒内连击

    public PlayerPrimaryAttack(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    { 
        base.Enter();
        AudioManager.instance.PlaySFX(0, player.transform);
        
        xInput = 0;

        if(comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)//连击
        {
            comboCounter = 0;
        }

        if (comboCounter == 2)
        {
            player.attackTransform.position += new Vector3(0.7f, 0, 0);
        }
        
        player.animator.SetInteger("ComboCounter", comboCounter);


        #region Choose attack direction
        float attackDir = player.facingDir;

        if(xInput!=0)
        {
            attackDir = xInput;
        }
        #endregion

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);//攻击动作偏移

        stateTimer = 0.1f;
       
    }

    public override void Exit()
    {
        base.Exit();

        //player.StartCoroutine("BusyFor", 0.15f);
        player.StartCoroutine("BusyFor", 0.1f);//lock
    
        comboCounter++;
        lastTimeAttacked = Time.time;
        
        if(comboCounter > 2)
            player.attackTransform.position -= new Vector3(0.7f, 0, 0);
        
        AudioManager.instance.StopSFX(0);
        
    }

    public override void Update()
    { 
        base.Update();

        if(stateTimer < 0)
        {
            player.SetVelocity(0,rb.velocity.y);
        }

        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
