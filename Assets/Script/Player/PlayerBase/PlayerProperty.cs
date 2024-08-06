using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<Property>> propertyDict;

    public float maxHP;
    public float hpValue;
    public float atkValue;

    public float atkBonus = 1;
    public float weaponBonus = 1;

    private bool ability1 = false;
    private bool ability2 = false;

    public void Start() {
        //currentHP = maxHP;
        HealthUI.Instance.setMaxHealth((int)maxHP);
        HealthUI.Instance.setCurrentHealth((int)hpValue);

        propertyDict = new Dictionary<PropertyType, List<Property>>();
        propertyDict.Add(PropertyType.MaxHPValue, new List<Property>());
        propertyDict.Add(PropertyType.HPValue, new List<Property>());
        propertyDict.Add(PropertyType.AttackValue, new List<Property>());
    }
    public void AddProperty(PropertyType pt, float value) {
        switch (pt) {
            case PropertyType.MaxHPValue:
                maxHP += value;
                hpValue += value;
                HealthUI.Instance.setMaxHealth((int)maxHP);
                return;
            case PropertyType.HPValue:
                hpValue += value;
                hpValue = Mathf.Clamp(hpValue, 0, maxHP);
                if (value <= 0 && ability2)
                {
                    Ab2TakeDamage();
                }
                HealthUI.Instance.setCurrentHealth((int)hpValue);
                return;
            case PropertyType.AttackValue:
                atkValue += value;
                return;
            case PropertyType.AttackBonus:
                atkValue += value;
                return;
            case PropertyType.AbilityIndex:
                UnlockAbility((int)value);
                return;
        }

        List<Property> list;
        propertyDict.TryGetValue(pt, out list);
        list.Add(new Property(pt, value));
    }
    public void RemoveProperty(PropertyType pt, float value) {
        switch (pt) {
            case PropertyType.MaxHPValue:
                maxHP -= value;
                hpValue = Mathf.Clamp(hpValue, 0, maxHP);
                HealthUI.Instance.setMaxHealth((int)maxHP);
                return;
            case PropertyType.HPValue:
                hpValue -= value;
                hpValue = Mathf.Clamp(hpValue, 0, maxHP);
                if (value >= 0 && ability2)
                {
                    Ab2TakeDamage();
                }
                HealthUI.Instance.setCurrentHealth((int)hpValue);
                return;
            case PropertyType.AttackValue:
                atkValue -= value;
                return;
            case PropertyType.AttackBonus:
                atkValue -= value;
                return;
            case PropertyType.AbilityIndex:
                lockAbility((int)value);
                return;
        }

        List<Property> list;
        propertyDict.TryGetValue(pt, out list);
        list.Remove(list.Find(x => x.value == value));
    }

    public void UseItem(ItemSO itemSO) {
        foreach (Property p in itemSO.propertyList) {
            AddProperty(p.propertyType, p.value);
        }
    }

    public void relife() {
        hpValue = maxHP;
        HealthUI.Instance.setCurrentHealth((int)hpValue);
    }

    public void UnlockAbility(int index) {
        switch (index) {
            case 1:
                ability1 = true;
                return;
            case 2:
                ability2 = true;
                timeTrackerCoroutine = StartCoroutine(TimeTracker());
                return;
            case 3:
                GetComponent<Player>().hasAb3 = true;
                return;
            case 4:
                GetComponent<Player>().attackCheckRadius = GetComponent<Player>().attackCheckRadius * 1.25f;
                return;
        }
    }

    public void lockAbility(int index) {
        switch (index) {
            case 1:
                ability1 = false;
                return;
            case 2:
                DisableAbility2();
                return;
            case 3:
                GetComponent<Player>().hasAb3 = false;
                return;
            case 4:
                GetComponent<Player>().attackCheckRadius = GetComponent<Player>().attackCheckRadius * 0.8f;
                return;
        }
    }

    public float getAtkResult()
    {
        float temp = ability1 ? 0.4f * ((maxHP - hpValue) / maxHP) : 0f;
        float temp2 = 0f;
        if (ability2)
        {
            temp2 = (float)(Mathf.Floor(timeSinceLastDamage / 10f) + 2);
        }

        return atkValue * (atkBonus + temp) * weaponBonus + temp2;
    }
    
    private float timeSinceLastDamage = 0f; // 未受伤时间
    private float maxTime = 40f;         // 最大未受伤时间
    //private bool isBuffActive = true;    // 加成是否激活

    private Coroutine buffCoroutine;
    private Coroutine timeTrackerCoroutine;

    private IEnumerator TimeTracker()
    {
        while (true)
        {
            if (ability2)
            {
                timeSinceLastDamage += 1f; // 每秒增加一次
                if (timeSinceLastDamage > maxTime)
                    timeSinceLastDamage = maxTime;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void Ab2TakeDamage()
    {
        if (ability2)
        {
            if (buffCoroutine != null)
                StopCoroutine(buffCoroutine);

            buffCoroutine = StartCoroutine(DisableBuffForSeconds(10));
        }
    }

    private IEnumerator DisableBuffForSeconds(int seconds)
    {
        ability2 = false;
        timeSinceLastDamage = 0f;
        yield return new WaitForSeconds(seconds);
        ability2 = true;
    }
    
    public void DisableAbility2()
    {
        // 停止所有协程
        if (timeTrackerCoroutine != null)
        {
            StopCoroutine(timeTrackerCoroutine);
            timeTrackerCoroutine = null;
        }
        
        if (buffCoroutine != null)
        {
            StopCoroutine(buffCoroutine);
            buffCoroutine = null;
        }

        // 重置状态
        ability2 = false;
        timeSinceLastDamage = 0f;
    }
}
