using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Teleporter : MonoBehaviour
{
    public GameObject targetPos;
    
    private bool playerInArea = false;
    private bool firstHint = false;
    
    public GameObject ImagePrefab;
    public FloatingImageController floatingImageController;
    public Transform iconSpawnPoint;
    
    private GetCurrentLayer getCurrentLayer;
    
    private VideoPlayer videoPlayer;

    public void Start()
    {
        getCurrentLayer = GetComponent<GetCurrentLayer>();
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void Update()
    {
        if (playerInArea && !firstHint && getCurrentLayer.intersectionCount >= 2)
        {
            firstHint = true;
            imageSpawn();
        }
        
        if (playerInArea && Input.GetKeyDown(KeyCode.E) && getCurrentLayer.intersectionCount >= 2)
        {
            teleport();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player")) 
        {
            playerInArea = true;
            if (ImagePrefab != null && iconSpawnPoint != null && getCurrentLayer.intersectionCount >= 2)
            {
                imageSpawn();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player")) 
        {
            playerInArea = false;
        }
    }

    public void imageSpawn()
    {
        floatingImageController.floatingImagePrefab = ImagePrefab;
        floatingImageController.SpawnFloatingImage(iconSpawnPoint);   
    }

    public void teleport()
    {
        if (AudioManager.instance.musicSource)
        {
            AudioManager.instance.musicSource.Stop();
        }
        StartCoroutine(WaitForStop());
        CameraManager.instance.SwitchToCamera2();
        //StartCoroutine(WaitForStop());
    }
    
    private IEnumerator WaitForStop()
    {
        videoPlayer.Play();
        yield return new WaitForSeconds(2.2f);
        Player player = GlobalManager.instance.player;
        player.transform.position = targetPos.transform.position;
        videoPlayer.Stop();
    }

}
