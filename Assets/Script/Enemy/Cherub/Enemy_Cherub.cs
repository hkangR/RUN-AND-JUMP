using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cherub : Enemy
{
    [SerializeField] public float launchSpeed;
    [SerializeField] public float flyTime;
    [SerializeField] public List<GameObject> photonBullet;
    [SerializeField] public bool isCreatingBullet;
    
    #region States
    public CherubIdleState idleState { get; private set; }
    public CherubFlyState flyState { get; private set; }
    public CherubFallState fallState { get; private set; }
    public CherubAttackState attackState { get; private set; }
    public CherubDeadState deadState { get; private set; }
    
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        idleState = new CherubIdleState(this, stateMachine, "Idle", this);
        flyState = new CherubFlyState(this, stateMachine, "Fly", this);
        fallState = new CherubFallState(this, stateMachine, "Fall", this);
        attackState = new CherubAttackState(this, stateMachine, "Attack", this);
        deadState = new CherubDeadState(this, stateMachine, "Die", this);
        //stunnedState = new SkeletonStunnedState(this, stateMachine, "Stun", this);
    }
    
    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0f;

        stateMachine.Initialize(idleState);
    }
    
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, playerDetectedDistance);
    }


    public bool CherubPlayerDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position, playerDetectedDistance, whatIsPlayer);
    
        // 检查是否在圆形区域内检测到了玩家
        foreach (Collider2D collider in colliders)
        {
            // 如果检测到了玩家，返回 true
            if (collider.CompareTag("Player")) 
            {
                return true;
            }
        }
        // 如果没有检测到玩家，返回 false
        return false;
    }
    
    
    public IEnumerator CreatePhotonBullet()
    {
        isCreatingBullet = true;
        for (int i = 0; i < photonBullet.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pb = Instantiate(photonBullet[i], transform.position, Quaternion.identity);
            pb.GetComponent<BulletControl>().enemy = gameObject;
        }
        
        isCreatingBullet = false;
        //stateMachine.ChangeState(fallState);
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
            //Instantiate(mask, maskPos, Quaternion.identity);//原地生成mask
        }
        gameObject.SetActive(false);
        
    }
    
}
