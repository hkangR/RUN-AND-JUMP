using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class InputCheckArea : MonoBehaviour
{
    public List<int> predefinedSequence;
    //private readonly List<int> cheatCode = new List<int> { 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0 };

    private List<int> playerInputSequence = new List<int>();
    private bool playerInArea = false;
    
    public GameObject successObject;
    
    public Transform successSpawnPoint = null;

    public FloatingImageController floatingImageController = null;
    public GameObject ImagePrefab = null;
    public Transform iconSpawnPoint = null;

    public bool PropertyCheck = false;
    public Property PropertyBonus = null;

    public ItemGetHintController itemGetHintController = null;
    
    public GameObject mask;
    
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

    void Update() 
    {
        if (playerInArea) 
        {
            if (Input.GetKeyDown(KeyCode.Alpha0)) 
            {
                AddInput(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1)) 
            {
                AddInput(1);
            }
        }
    }

    void AddInput(int input) 
    {
        playerInputSequence.Add(input);

        if (playerInputSequence.Count > predefinedSequence.Count) 
        {
            playerInputSequence.RemoveAt(0);
        }

        if (IsSequenceMatch()) 
        {
            OnSuccess();
        }
    }

    bool IsSequenceMatch() 
    {
        if (playerInputSequence.Count != predefinedSequence.Count) 
        {
            return false;
        }

        for (int i = 0; i < predefinedSequence.Count; i++) 
        {
            if (playerInputSequence[i] != predefinedSequence[i]) 
            {
                return false;
            }
        }
        return true;
    }

    void OnSuccess()
    {
        //Debug.Log("Puzzle Solved!");
        Player player = GlobalManager.instance.player;
        
        if (successObject != null && successSpawnPoint != null)
        {
            //Instantiate(successObject, successSpawnPoint.position, successSpawnPoint.rotation);
            itemGetHintController.ShowUI(3f, true);
            itemGetHintController.init(successObject.GetComponent<PickableObject>().itemSO);
            player.UseItem(successObject.GetComponent<PickableObject>().itemSO);
        }

        if (ImagePrefab != null && iconSpawnPoint != null)
        {
            floatingImageController.floatingImagePrefab = ImagePrefab;
            floatingImageController.SpawnFloatingImage(iconSpawnPoint);   
        }

        if (PropertyCheck && PropertyBonus != null)
        {
            //Player player = PlayerManager.instance.player;
            player.GetComponent<PlayerProperty>().AddProperty(PropertyBonus.propertyType,PropertyBonus.value);
        }
        
        GameObject mask1 = Instantiate(mask, transform.position,Quaternion.identity);
        mask1.transform.localScale = new Vector3(2f,2f,2f);
        
        GameObject mask2 = Instantiate(mask, transform.position,Quaternion.identity);
        mask2.transform.localScale = new Vector3(2f,2f,2f);

        Destroy(gameObject);
    }
}
