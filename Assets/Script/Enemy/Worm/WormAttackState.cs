using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormAttackState : EnemyState
{
    private Enemy_Worm enemy;

    public WormAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Worm enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.sfxSource = enemy.GetComponent<AudioSource>();
        AudioManager.instance.PlaySFX("WormAttack");
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttack = Time.time;
        AudioManager.instance.sfxSource.Stop();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(0,0);

        if(triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
