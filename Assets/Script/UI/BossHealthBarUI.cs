using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{    
    public static BossHealthBarUI Instance { get; private set; }
    
    [SerializeField] public Image progress;
    [SerializeField] public Image bg;
    [SerializeField] public Sprite stageOnePrpogress;
    [SerializeField] public Sprite stageTwoPrpogress;
    
    public Enemy enemy;

    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHealth;

    private bool isStageTwo = false;

    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(Instance); return; }
        Instance = this;
    }
    
    private void Start()
    {
        gameObject.SetActive(true);
        ChangeProgressSprite(stageOnePrpogress); 
        currentHealth = enemy.GetComponent<EnemyProperty>().hpValue;
        maxHealth = enemy.GetComponent<EnemyProperty>().maxHP;
        UpdateHealthBar();
    }

    //public void Update()
    //{
    //     currentHealth = enemy.GetComponent<EnemyProperty>().hpValue;
    //    maxHealth = enemy.GetComponent<EnemyProperty>().maxHP;
    //    UpdateHealthBar();
    //}

    public void UpdateHealthBar()
    {
        if (maxHealth == 0)
        {
            Debug.LogError("MAX HEALTH IS ZERO");
            return;
        }
        currentHealth = enemy.GetComponent<EnemyProperty>().hpValue;
        maxHealth = enemy.GetComponent<EnemyProperty>().maxHP;
        float fillAmount = currentHealth / maxHealth;
        progress.fillAmount = fillAmount;

        if (currentHealth == 0 && !isStageTwo)
        {
            isStageTwo = true;
            ChangeProgressSprite(stageTwoPrpogress);
        }
    }

    public void ChangeProgressSprite(Sprite newSprite)
    {
        progress.sprite = newSprite;
    }
}