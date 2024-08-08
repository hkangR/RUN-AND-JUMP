using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VictorymenuController : MonoBehaviour
{
    public Image blackLayer;
    public GameObject menuUI;
    
    public float fadeDuration = 2.0f;
    
    public void Start()
    {
        if (blackLayer != null && menuUI != null)
        {
            InitializeTransparency();
            //StartCoroutine(FadeInAndOut());
        }
    }

    public void Show()
    {
        blackLayer.transform.position = new Vector3(blackLayer.transform.position.x, blackLayer.transform.position.y - 2000, blackLayer.transform.position.z);
        menuUI.transform.position = new Vector3(menuUI.transform.position.x, menuUI.transform.position.y - 2000, menuUI.transform.position.z);
        
        if (blackLayer != null && menuUI != null)
        {
            DeactivateOtherChildren();
            StartCoroutine(FadeInAndOut());
        }
    }

    void InitializeTransparency()
    {
        Color color1 = blackLayer.color;
        color1.a = 0f;
        blackLayer.color = color1;
        
        SetAlphaForAllChildren(menuUI.transform, 0f);
    }

    IEnumerator FadeInAndOut()
    {
        // Step 1: Fade in object1
        yield return StartCoroutine(FadeIn(blackLayer, fadeDuration));

        // Step 2: Keep object1 fully opaque and fade in object2's children
        yield return StartCoroutine(FadeInSecondObject(menuUI.transform, fadeDuration));
    }

    IEnumerator FadeIn(Image image, float duration)
    {
        Color color = image.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            image.color = color;
            yield return null;
        }

        color.a = 1f; // Ensure the final alpha is set to fully opaque
        image.color = color;
    }

    IEnumerator FadeInSecondObject(Transform targetTransform, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            SetAlphaForAllChildren(targetTransform, Mathf.Clamp01(elapsedTime / duration));
            yield return null;
        }

        SetAlphaForAllChildren(targetTransform, 1f); // Ensure all children of object2 are fully visible
    }

    void SetAlphaForAllChildren(Transform parent, float alpha)
    {
        foreach (Transform child in parent)
        {
            var image = child.GetComponent<Image>();
            if (image != null)
            {
                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }
            
            var tmp = child.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                Color color = tmp.color;
                color.a = alpha;
                tmp.color = color;
            }

            var canvasGroup = child.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = alpha;
            }

            // Recursively set alpha for all children
            SetAlphaForAllChildren(child, alpha);
        }
    }
    
    public void OnReturn_Clicked()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    
    void DeactivateOtherChildren()
    {        
        if (gameObject != null && gameObject.transform.parent != null && gameObject.transform.parent.parent != null)
        {
            Transform grandparentTransform = gameObject.transform.parent.parent;
            Transform parentTransform = gameObject.transform.parent;

            foreach (Transform sibling in grandparentTransform)
            {
                if (sibling != parentTransform)
                {
                    sibling.gameObject.SetActive(false);
                }
            }
        }
    }
}
