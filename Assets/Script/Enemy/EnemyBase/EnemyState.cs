using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    private string animBoolName;

    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;

    protected float stateTimer;//��player����
    protected bool triggerCalled;

    protected Rigidbody2D rb;
    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;  

    }
    public virtual void Enter()
    {
        rb = enemyBase.rb;
        enemyBase.animator.SetBool(animBoolName, true);
        triggerCalled = false;  
        
    }
    public virtual void Exit()
    {
        enemyBase.animator.SetBool(animBoolName, false);
        enemyBase.AssignLastAnimName(animBoolName);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true; 
    }
}
