using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] public GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private float bulletSpeed;
    //private EnemyProperty enemy;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Vector3 originalPos;
    
    private void Awake()
    {
        //enemy = transform.parent.gameObject;
        player = GameObject.Find("Player1");
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        originalPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("hit");   
            // 碰撞到玩家时销毁子弹
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
