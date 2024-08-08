using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FloatingImageController : MonoBehaviour 
{
    public GameObject floatingImagePrefab;
    public float displayDuration = 1f; // 图片显示的时间
    public float fadeDuration = 1f; // 图片淡出的时间

    private void Awake()
    {
    }

    public void SpawnFloatingImage(Transform targetTransform) 
    {
        // 在目标Transform的位置生成图片
        GameObject floatingImage = Instantiate(floatingImagePrefab, targetTransform.position, Quaternion.identity);
        StartCoroutine(FadeInAndOut(floatingImage));
    }

    private IEnumerator FadeInAndOut(GameObject imageObject) 
    {
        SpriteRenderer spriteRenderer = imageObject.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        // 图片淡入
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, normalizedTime);
            yield return null;
        }
        spriteRenderer.color = originalColor;

        // 等待显示时间
        yield return new WaitForSeconds(displayDuration);

        // 图片淡出
        for (float t = 0; t < fadeDuration; t += Time.deltaTime) 
        {
            float normalizedTime = t / fadeDuration;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - normalizedTime);
            yield return null;
        }
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        // 销毁图片对象
        Destroy(imageObject);
    }
}
