using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : Enemy
{
    [SerializeField] private bool _isLeftHand;
    [SerializeField] private float groundedTime;
    [SerializeField] private Vector3 originPos;//初始位置
    
    [SerializeField] public float yOffset;
    [SerializeField] public bool isHitting;
    [SerializeField] public Vector3 hitOffset;
    [SerializeField] private float hitSpeed;
    
    [SerializeField] public float floatSpeed;//左右浮动速度
    [SerializeField] public float floatTime;//左右浮动时间
    public float shakeDuration = 0.5f; // 摇晃持续时间

    
    #region States
    public BossHandIdleState idleState { get;private set; }//出场和过度状态
    public BossHandReady ready { get; private set; }
    public BossHandHitState hitState { get; private set; }
    #endregion
    
    
    protected override void Awake()
    {
        base.Awake();
        //enemyProperty = GetComponent<EnemyProperty>();
        idleState = new BossHandIdleState(this, stateMachine, "RightIdle", this);
        ready = new BossHandReady(this, stateMachine, "RightReady", this);
        hitState = new BossHandHitState(this, stateMachine, "RightHit", this);
    }
    
    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0f;//boss不受重力影响
        originPos = transform.position;
        stateMachine.Initialize(idleState);
    }
    
    //拍击地面（固定位置），大地震撼（在地面上则收到伤害）
    
    //在玩家上方浮动，拍击玩家（拍击到则收到伤害）
    public IEnumerator Hit()
    {
        //yield return StartCoroutine(HandShake());
        isHitting = true;
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0f-yOffset), hitSpeed * Time.deltaTime);//拍击
        yield return new WaitForSeconds(groundedTime);
        

        yield return new WaitForSeconds(groundedTime);
        
        isHitting = false;
        transform.position = Vector3.Lerp(transform.position, originPos,  hitSpeed * Time.deltaTime);//回到原位
        stateMachine.ChangeState(idleState);
    }


    
   
    
}
