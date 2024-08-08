using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudDeadState : EnemyState
{
    private Enemy_Mud enemy;
    public MudDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Mud enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.sfxSource = enemy.GetComponent<AudioSource>();
        AudioManager.instance.PlaySFX("MudDeath");
        
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
