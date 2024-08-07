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

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (controlOrMap && collision.CompareTag("Player"))
        {
            itemGetHintController.HideC();
        }
    }

    private void TriggerEffect()
    {
        //Player player = PlayerManager.instance.player;

        if (controlOrMap)
        {
            itemGetHintController.initC(storedSrting);
            itemGetHintController.ShowC();
        }
        else
        {
            itemGetHintController.initM(storedSrting);
            itemGetHintController.ShowUI(5f,false);
            //Destroy(gameObject);
        }
    }
}
