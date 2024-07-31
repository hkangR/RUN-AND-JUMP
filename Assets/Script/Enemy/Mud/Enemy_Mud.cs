using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mud : Enemy
{
    
    [SerializeField] public float rushSpeed;//for battle
    [SerializeField] public float dashTime;//for attack
    [SerializeField] public float dashSpeed;//for attack
    #region States
    public MudIdleState idleState { get; private set; }
    //public MudRushState moveState { get; private set; }
    public MudBattleState battleState { get; private set; }//found player and rush to attack
    public MudAttackState attackState { get; private set; }
    public MudDeadState deadState { get; private set; }
    
    //public WormStunnedState stunnedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new MudIdleState(this, stateMachine, "Idle", this);
        battleState = new MudBattleState(this, stateMachine, "Rush", this);
        attackState = new MudAttackState(this, stateMachine, "Attack", this);
        deadState = new MudDeadState(this, stateMachine, "Die", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();

        StartCoroutine("WaitForDie");
    }

    private IEnumerator WaitForDie()
    {
        stateMachine.ChangeState(deadState);

        yield return new WaitForSeconds(0.5f);
        
        Vector3 maskPos = transform.position;
        //stateMachine.ChangeState(deadState);
        if (canCreateMask)
        {
            Instantiate(mask, maskPos, Quaternion.identity);//原地生成mask
            Instantiate(mask, maskPos, Quaternion.identity);//原地生成mask
        }
        gameObject.SetActive(false);
        
    }
    
    
    public void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == Tag.PLAYER) 
        {
            Player player = collision.gameObject.GetComponent<Player>();
            StartCoroutine(MakeDamage(player));
        }
    }

    private IEnumerator MakeDamage(Player player)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);
        CauseDamage(player);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), false);
    }


}
