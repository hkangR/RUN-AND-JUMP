using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttack : PlayerState
{

    public PlayerAirAttack(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    { 
        base.Enter();
        AudioManager.instance.PlaySFX(0, player.transform);

        player.canDoubleJump = false;
        
        
        xInput = 0;
        
        #region Choose attack direction
        float attackDir = player.facingDir;

        if(xInput!=0)
        {
            attackDir = xInput;
        }
        #endregion
        

        stateTimer = 0.1f;
       
    }

    public override void Exit()
    {
        base.Exit();

        //player.StartCoroutine("BusyFor", 0.15f);
        player.StartCoroutine("BusyFor", 0.1f);//lock
        
        //player.canDoubleJump = true;
        
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
