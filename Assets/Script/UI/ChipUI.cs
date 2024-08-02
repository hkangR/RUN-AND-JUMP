
using UnityEngine;

public class ChipUI : MonoBehaviour
{
    public static ChipUI instance { get; private set; }

    public GameObject chipPrefab;
    
    private int chipNum = 0;

    private void Awake() 
    {
        if (instance != null && instance != this) { Destroy(instance); return; }
        instance = this;
    }
    public void PickChip()
    {
        chipNum++;
        UpdateChip();
    }

    private void Remove()
    {
        chipNum--;
        UpdateChip();
    }
    
    private void UpdateChip() 
    {
        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < chipNum; i++)
        {
            GameObject newChip = Instantiate(chipPrefab, transform);
            newChip.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }
}
