using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetHintController : MonoBehaviour
{
    public Image chipIcon;
    public TextMeshProUGUI tipTitle;
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI fieldtext;
    public TextMeshProUGUI propertyText;

    public TextMeshProUGUI infoNameUnlock;
    public TextMeshProUGUI infoTextUnlock;
    //public TextMeshProUGUI infoTutorialUnlock;
    
    public TextMeshProUGUI infoNameMap;
    
    public TextMeshProUGUI infoTextControl;

    public GameObject itemUI;
    public GameObject infoUI;

    public GameObject infoUIUnlock;
    public GameObject infoUIMap;
    public GameObject infoUIControl;

    public void Start()
    {
        itemUI.transform.position = new Vector3(itemUI.transform.position.x, itemUI.transform.position.y - 2000, itemUI.transform.position.z);
        infoUI.transform.position = new Vector3(infoUI.transform.position.x, infoUI.transform.position.y - 2000, infoUI.transform.position.z);
        clear();
    }

    public void init(ItemSO itemSO) //ITEM
    {
        if (itemSO == null)
        {
            Debug.Log("SO is empty!");
        }
        
        switch (itemSO.itemType)
        {
            case ItemType.Consumable:
                tipTitle.text = "特殊获得";
                break;
            case ItemType.Chip:
                tipTitle.text = "芯片获得";
                break;
        }
        
        chipIcon.sprite = itemSO.prefab.GetComponent<SpriteRenderer>().sprite;
        itemText.text = itemSO.name;
        fieldtext.text = itemSO.field;
        propertyText.text = itemSO.description;
        //itemUI.SetActive(true);
    }
    
    public void init(string name, string text) //UNLOCK
    {
        infoNameUnlock.text = name;
        infoTextUnlock.text = text;
        //infoUI.SetActive(true);
        infoUIUnlock.SetActive(true);
    }
    
    public void initM(string name) //MAP
    {
        infoNameMap.text = name;
        //infoUI.SetActive(true);
        infoUIMap.SetActive(true);
    }
    
    public void initC(string text) //CONTROL
    {
        infoTextControl.text = text;
        //infoUI.SetActive(true);
        infoUIControl.SetActive(true);
    }

    public void clear()
    {
        itemUI.SetActive(false);
        infoUI.SetActive(false);
        infoUIUnlock.SetActive(false);
        infoUIMap.SetActive(false);
        infoUIControl.SetActive(false);
        //HintUI = null;
    }
    
    private GameObject HintUI;
    private Coroutine showUICoroutine;
    
    public void ShowUI(float duration, bool ItemOrInfo)
    {
        if (showUICoroutine != null)
        {
            StopCoroutine(showUICoroutine);
            clear();
        }
        if (ItemOrInfo) { HintUI = itemUI; }
        else { HintUI = infoUI; }
        showUICoroutine = StartCoroutine(DisplayUI(duration));
    }

    public void ShowC()
    {
        if (!infoUI.activeSelf)
        {
            infoUI.SetActive(true);
        }
    }

    public void HideC()
    {
        if (infoUIControl.activeSelf)
        {
            infoUIControl.SetActive(false);
        }
        if (showUICoroutine == null)
        {
            clear();
        }
    }

    private IEnumerator DisplayUI(float duration)
    {
        HintUI.SetActive(true);
        yield return new WaitForSeconds(duration);
        HintUI.SetActive(false);
        clear();
        showUICoroutine = null;
    }
}
