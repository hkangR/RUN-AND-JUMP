using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
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
}