using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormDeadState : EnemyState
{
    private Enemy_Worm enemy;
    public WormDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Worm enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.sfxSource = enemy.GetComponent<AudioSource>();
        AudioManager.instance.PlaySFX("WormDeath");
    }
    

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.sfxSource.Stop();
    }

    public override void Update()
    {
        base.Update();
    }
}
