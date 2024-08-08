using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FloatingImageController : MonoBehaviour 
{
    public GameObject floatingImagePrefab;
    public float displayDuration = 1f; // ͼƬ��ʾ��ʱ��
    public float fadeDuration = 1f; // ͼƬ������ʱ��

    private void Awake()
    {
    }

    public void SpawnFloatingImage(Transform targetTransform) 
    {
        // ��Ŀ��Transform��λ������ͼƬ
        GameObject floatingImage = Instantiate(floatingImagePrefab, targetTransform.position, Quaternion.identity);
        StartCoroutine(FadeInAndOut(floatingImage));
    }

    private IEnumerator FadeInAndOut(GameObject imageObject) 
    {
        SpriteRenderer spriteRenderer = imageObject.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        // ͼƬ����
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, normalizedTime);
            yield return null;
        }
        spriteRenderer.color = originalColor;

        // �ȴ���ʾʱ��
        yield return new WaitForSeconds(displayDuration);

        // ͼƬ����
        for (float t = 0; t < fadeDuration; t += Time.deltaTime) 
        {
            float normalizedTime = t / fadeDuration;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - normalizedTime);
            yield return null;
        }
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        // ����ͼƬ����
        Destroy(imageObject);
    }
}
