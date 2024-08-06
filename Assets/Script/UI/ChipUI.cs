
using UnityEngine;

public class ChipUI : MonoBehaviour
{
    public static ChipUI instance { get; private set; }

    //public GameObject chipPrefab;
    
    private int chipNum = 0;

    private void Awake() 
    {
        if (instance != null && instance != this) { Destroy(instance); return; }
        instance = this;
    }
    public void PickChip(GameObject newPrefab)
    {
        chipNum++;
        UpdateChip(newPrefab);
    }

    private void Remove(GameObject newPrefab)
    {
        chipNum--;
        UpdateChip(newPrefab);
    }
    
    private void UpdateChip(GameObject newPrefab) 
    {
        GameObject newChip = Instantiate(newPrefab, transform);
        newChip.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
}
