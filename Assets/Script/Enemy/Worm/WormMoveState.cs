using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMoveState : EnemyGroundState
{
    public WormMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Worm enemy):base(enemyBase,stateMachine,animBoolName,enemy)
    {
       
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.patrolTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        //Debug.Log("Move State");
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir , rb.velocity.y);

        if (Vector2.Distance(enemy.transform.position, enemy.selfPlayer.transform.position) < 0.2)
        {
            stateMachine.ChangeState(enemy.battleState);
        }

        if (stateTimer < 0)
        {
            enemy.Flip();
            stateTimer = enemy.patrolTime;
            stateMachine.ChangeState(enemy.idleState);
        }

        if(enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
            
        }

    }
}
