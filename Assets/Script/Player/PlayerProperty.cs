using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<Property>> propertyDict;

    public float maxHP;
    public float hpValue;
    public float atkValue;

    public float atkBonus;
    public float atkResult => atkValue + atkValue * atkBonus;

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
                return;
            case PropertyType.HPValue:
                hpValue += value;
                hpValue = Mathf.Clamp(hpValue, 0, maxHP);
                HealthUI.Instance.setCurrentHealth((int)hpValue);
                return;
            case PropertyType.AttackValue:
                atkValue += value;
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
                return;
            case PropertyType.HPValue:
                hpValue -= value;
                hpValue = Mathf.Clamp(hpValue, 0, maxHP);
                HealthUI.Instance.setCurrentHealth((int)hpValue);
                return;
            case PropertyType.AttackValue:
                atkValue -= value;
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
}
