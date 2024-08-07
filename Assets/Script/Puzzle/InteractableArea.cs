using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableArea : MonoBehaviour
{
    private bool playerInArea = false;

    public int unlockIndex = 0;
    public string nameSentForShow = "";
    public string textSentForShow = "";
    public string hintSentForShow = "";

    public GameObject ImagePrefab;
    public FloatingImageController floatingImageController;
    public Transform iconSpawnPoint;
    
    public ItemGetHintController itemGetHintController;

    void Update()
    {
        if (playerInArea)
        {
            if (ImagePrefab != null && iconSpawnPoint != null)
            {
                floatingImageController.floatingImagePrefab = ImagePrefab;
                floatingImageController.SpawnFloatingImage(iconSpawnPoint);   
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerEffect();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player")) 
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player")) 
        {
            playerInArea = false;
        }
    }

    private void TriggerEffect()
    {
        Player player = GlobalManager.instance.player;
        switch (unlockIndex)
        {
            case 1: //二段跳
                GlobalManager.instance.player.canDoubleJump = true;
                break;
            case 2:
                GlobalManager.instance.player.canDash = true;
                break;
        }
        
        itemGetHintController.ShowUI(3f, false);
        itemGetHintController.init(nameSentForShow, textSentForShow);
        itemGetHintController.initC(hintSentForShow);
        
        Destroy(gameObject);
    }
}
