using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour 
{
    public static HealthUI Instance { get; private set; }

    public GameObject heartPrefab;
    public int maxHealth;
    private List<Heart> hearts = new List<Heart>();

    public int currentHealth;

    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(Instance); return; }
        Instance = this;
    }

    public void setMaxHealth(int newHealth) 
    {
        maxHealth = newHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();
    }

    public void setCurrentHealth(int newHealth) 
    {
        currentHealth = newHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();
    }

    private void UpdateHearts() 
    {
        
        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }

        for (float i = 0; i < (float)maxHealth / 2f; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, transform);
            Heart heart = heartObj.GetComponent<Heart>();
            hearts.Add(heart);
            if (!heart)
            {
                return;
            }
            if (i * 2 + 1 < currentHealth) 
            {
                heart.SetHeartState(2);
            }
            else if (i * 2 + 1 == currentHealth) 
            {
                heart.SetHeartState(1);
            }
            else 
            {
                heart.SetHeartState(0);
            }
        }
    }
}