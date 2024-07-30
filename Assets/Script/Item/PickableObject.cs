using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour{
    public ItemSO itemSO;
    public void Interact(){
        //GameObject.FindGameObjectWithTag(Tag.PLAYER).GetComponent<Player>().UseItem(itemSO);
        Destroy(this.gameObject);
    }
}
