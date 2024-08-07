using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemSO : ScriptableObject {
    public int id;
    public new string name;
    public string description;
    public string field;
    public ItemType itemType;
    public List<Property> propertyList;
    //public Sprite icon;
    public GameObject prefab;
}

public enum ItemType {
    Weapon,
    Consumable,
    Chip
}

[Serializable]
public class Property {
    public PropertyType propertyType;
    public float value;
    public Property() {

    }
    public Property(PropertyType propertyType, float value) {
        this.propertyType = propertyType;
        this.value = value;
    }
    
}




public enum PropertyType {
    MaxHPValue,
    HPValue,
    AttackValue,
    AttackBonus,
    AbilityIndex,
}