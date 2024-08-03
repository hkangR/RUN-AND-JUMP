using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<Property>> propertyDict;

    public float hpValue;
    public float maxHP;
    public float atkValue;

    public float atkBonus = 1;
    public float atkResult => atkValue * (1 + atkBonus);

    public void Start() 
    {
        propertyDict = new Dictionary<PropertyType, List<Property>>();
        propertyDict.Add(PropertyType.MaxHPValue, new List<Property>());
        propertyDict.Add(PropertyType.HPValue, new List<Property>());
        propertyDict.Add(PropertyType.AttackValue, new List<Property>());
    }

    public void AddProperty(PropertyType pt, float value) 
    {
        switch (pt) 
        {
            case PropertyType.MaxHPValue:
                maxHP += value;
                hpValue += value;
                if (gameObject.CompareTag(Tag.Boss)) {
                    BossHealthBarUI.Instance.UpdateHealthBar();
                }
                return;
            case PropertyType.HPValue:
                hpValue += value;
                hpValue = Mathf.Clamp(hpValue, 0, maxHP);
                if (gameObject.CompareTag(Tag.Boss)) {
                    BossHealthBarUI.Instance.UpdateHealthBar();
                }
                return;
            case PropertyType.AttackValue:
                atkValue += value;
                return;
            case PropertyType.AttackBonus:
                atkValue += value;
                return;
        }

        List<Property> list;
        propertyDict.TryGetValue(pt, out list);
        list.Add(new Property(pt, value));
    }
    public void RemoveProperty(PropertyType pt, float value) 
    {
        switch (pt) 
        {
            case PropertyType.MaxHPValue:
                maxHP -= value;
                hpValue = Mathf.Clamp(hpValue, 0, maxHP);
                if (gameObject.CompareTag(Tag.Boss)) {
                    BossHealthBarUI.Instance.UpdateHealthBar();
                }
                return;
            case PropertyType.HPValue:
                hpValue -= value;
                hpValue = Mathf.Clamp(hpValue, 0, maxHP);
                if (gameObject.CompareTag(Tag.Boss)) {
                    BossHealthBarUI.Instance.UpdateHealthBar();
                }
                return;
            case PropertyType.AttackValue:
                atkValue -= value;
                return;
            case PropertyType.AttackBonus:
                atkValue -= value;
                return;
        }

        List<Property> list;
        propertyDict.TryGetValue(pt, out list);
        list.Remove(list.Find(x => x.value == value));
    }

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        Revive(1);
    //    }
    //}

    public void Revive(float time)
    {
        StartCoroutine(LerpValueOverTime(0, maxHP, time));
    }

    IEnumerator LerpValueOverTime(float start, float end, float time)
    {
        float elapsedTime = 0f;
        hpValue = start;

        while (elapsedTime < time)
        {
            hpValue = Mathf.Lerp(start, end, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            BossHealthBarUI.Instance.UpdateHealthBar();
            yield return null;
        }

        hpValue = end;
        BossHealthBarUI.Instance.UpdateHealthBar();
    }
}
