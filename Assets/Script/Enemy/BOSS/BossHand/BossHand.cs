using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : Enemy
{
    [SerializeField] private bool _isLeftHand;
    [SerializeField] private float groundedTime;
    [SerializeField] private Vector3 originPos;//初始位置
    
    [SerializeField] public float yOffset;
    [SerializeField] public Vector3 hitOffset;
    [SerializeField] private float hitSpeed;
    [SerializeField] public bool isHitting;
    
    [SerializeField] public float floatSpeed;//左右浮动速度
    [SerializeField] public float floatTime;//左右浮动时间
    public float shakeDuration = 0.5f; // 摇晃持续时间
    public float shakeMagnitude = 0.1f; // 摇晃幅度
    /*[SerializeField] public float shakeForce;//左右浮动时间
    [SerializeField] public Vector3 shakeOffset;*/
    
    #region States
    public BossHandIdleState idleState { get;private set; }//出场和过度状态
    public BossHandHitState hitState { get; private set; }
    #endregion
    
    
    protected override void Awake()
    {
        base.Awake();
        //enemyProperty = GetComponent<EnemyProperty>();
        idleState = new BossHandIdleState(this, stateMachine, "RightIdle", this);
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
    /*public IEnumerator Hit()
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

    public IEnumerator HandShake()
    {
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0;

        while (isHitting && elapsedTime < shakeDuration)
        {
            float xOffset = Mathf.Sin(Time.time * Mathf.PI * 10) * shakeMagnitude; // 通过正弦函数生成左右摇晃的偏移量
            transform.position = new Vector3(originalPosition.x + xOffset, transform.position.y, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 停止摇晃后恢复原位
        transform.position = originalPosition;
    }*/
    
    public IEnumerator Hit()
    {
        // 启动HandShake协程
        StartCoroutine(HandShake());
    
        isHitting = true;

        // 目标位置
        Vector3 targetPosition = new Vector3(transform.position.x, 0f - yOffset, transform.position.z);
        float elapsedTime = 0;
        Vector3 startingPosition = transform.position;

        // 平滑移动到目标位置
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime * hitSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(groundedTime);

        // 回到原位
        elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(targetPosition, originPos, elapsedTime * hitSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isHitting = false;
        stateMachine.ChangeState(idleState);
    }

    public IEnumerator HandShake()
    {
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0;

        while (isHitting && elapsedTime < shakeDuration)
        {
            float xOffset = Mathf.Sin(Time.time * Mathf.PI * 10) * shakeMagnitude; // 通过正弦函数生成左右摇晃的偏移量
            transform.position = new Vector3(originalPosition.x + xOffset, transform.position.y, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 停止摇晃后恢复原位
        transform.position = originalPosition;
    }
    
}
