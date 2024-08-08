using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;

    public GameObject menu;
    [FormerlySerializedAs("Background")] public GameObject background;

    private Animation animation;

    public void Start()
    {
        animation = GetComponentInChildren<Animation>();
    }

    public void StartGame()
    {
        menu.SetActive(false);
        StartCoroutine(WaitForLoadScene());
    }
    
    public void ShowSettings()
    {
        //gameObject.SetActive(false);
        settingsPanel.SetActive(true);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitForLoadScene()
    {
        animation.Play("Start");
        yield return new WaitForSeconds(4.1f);
        yield return StartCoroutine(ZoomIn());
        SceneManager.LoadScene("SampleScene");
    }
    
    private float duration = 1f;

    IEnumerator ZoomIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            background.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 2f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Camera.main.fieldOfView = targetFieldOfView;
    }
}
