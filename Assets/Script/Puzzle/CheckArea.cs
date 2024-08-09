using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    //private bool playerInArea = false;

    public bool controlOrMap = false;
    public string storedSrting = "";
    
    public ItemGetHintController itemGetHintController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            TriggerEffect();
        }
    }
    

    private void TriggerEffect()
    {
        //Player player = PlayerManager.instance.player;

        if (controlOrMap)
        {
            //itemGetHintController.initC(storedSrting);
            itemGetHintController.ShowUI(3f,false);
            itemGetHintController.initC(storedSrting);
            Destroy(gameObject);
        }
        else
        {
            //itemGetHintController.initM(storedSrting);
            itemGetHintController.ShowUI(1f,false);
            itemGetHintController.initM(storedSrting);
            //Destroy(gameObject);
        }
    }
}
