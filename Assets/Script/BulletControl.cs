using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private float bulletSpeed;
    //private EnemyProperty enemy;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Vector3 originalPos;
    
    private void Awake()
    {
        //enemy = transform.parent.gameObject;
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        enemy = GetComponentInParent<Enemy>().gameObject;
    }
    

    private void Start()
    {
        rb.gravityScale = 0f;
    }

    private void OnEnable()
    {
        AimPlayer();
        originalPos = transform.position;
        // 启动协程，在五秒后销毁子弹
        StartCoroutine(DestroyAfterDelay(3f));
    }

    public void AimPlayer()
    {
        //重新瞄准玩家
        transform.localPosition = Vector3.zero;
        player = GlobalManager.instance.player.gameObject;
        // 计算射向玩家的方向向量
        Vector3 shootDirection = (player.transform.position - originalPos).normalized;

        rb.velocity = shootDirection * bulletSpeed;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            //造成伤害
            enemy.GetComponent<Enemy>().CauseDamage(other.GetComponent<Player>());
            // 碰撞到玩家时销毁子弹
            //Destroy(gameObject);
            ObjectPool.instance.PushObject(gameObject);
            return;
        }
        
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Destroy(gameObject);
        ObjectPool.instance.PushObject(gameObject);
    }

}
