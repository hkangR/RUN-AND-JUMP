using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGetHit : PlayerState
{
    public PlayerGetHit(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
         
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.2f;
        player.canBeAttacked = false;
        
        player.StartCoroutine("FlashFX");
        rb.velocity = new Vector2(rb.velocity.x, player.beAttackForce);
    }

    public override void Exit()
    {
        base.Exit();
        player.canBeAttacked = true;
    }

    public override void Update()
    {
        base.Update();
        

        if(stateTimer < 0)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
