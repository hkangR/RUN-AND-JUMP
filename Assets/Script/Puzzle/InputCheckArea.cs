using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InputCheckArea : MonoBehaviour 
{
    [Tooltip("��Inspector�����õ�Ԥ�����������")]
    public List<int> predefinedSequence;
    //private readonly List<int> cheatCode = new List<int> { 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0 };

    private List<int> playerInputSequence = new List<int>();
    private bool playerInArea = false;

    [Tooltip("����ɹ�ʱ���ɵ�GameObject")]
    public GameObject successObject;

    [Tooltip("���ɳɹ������λ��")]
    public Transform successSpawnPoint;

    public FloatingImageController floatingImageController;
    public GameObject ImagePrefab;
    public Transform iconSpawnPoint;

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
        // ���ɳɹ�����
        if (successObject != null && successSpawnPoint != null)
        {
            Instantiate(successObject, successSpawnPoint.position, successSpawnPoint.rotation);
        }

        //����ͼƬ
        floatingImageController.floatingImagePrefab = ImagePrefab;
        floatingImageController.SpawnFloatingImage(iconSpawnPoint);

        Destroy(gameObject);
    }
}
