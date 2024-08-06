using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject baseMenu;
    [SerializeField] public GameObject controlMenu;
    
    public Slider volumeSlider;
    public TextMeshProUGUI graphicsText;
    public TextMeshProUGUI displayModeText;
    public TextMeshProUGUI languageText;
    public TextMeshProUGUI difficultyText;

    private string[] qualityLevels = { "低", "中", "高" };
    private string[] displayModes = { "窗口模式", "全屏模式" };
    private string[] languages = { "简体中文", "English" };
    private string[] difficulties = { "普通", "困难", "疯狂" };

    private int currentQualityIndex = 0;
    private int currentDisplayModeIndex = 0;
    private int currentLanguageIndex = 0;
    private int currentDifficultyIndex = 0;

    public GameObject baseBtn;
    public GameObject controlBtn;
    
    public Sprite btn_unclicked;
    public Sprite btn_clicked;
    
    public TextMeshProUGUI moveLeftText;
    public TextMeshProUGUI moveRightText;
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI dashText;
    public TextMeshProUGUI doubleJumpText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI pauseText;
    
    private string[] MoveLeftKeys = { "A", "摇杆←" };
    private string[] MoveRightKeys = { "D", "摇杆→" };
    private string[] JumpKeys = { "Space", "B" };
    private string[] DashKeys = { "Shift", "L1" };
    private string[] DoubleJumpKeys = { "连按Space", "连按B" };
    private string[] AttackKeys = { "LeftMous", "A" };
    private string[] PauseKeys = { "Q", "Home" };
    
    public GameObject keyboardBtn;
    public GameObject controllerBtn;

    void Start()
    {
        // 初始化UI
        volumeSlider.value = AudioListener.volume * 100;
        UpdateQualityText();
        UpdateDisplayModeText();
        UpdateLanguageText();
        UpdateDifficultyText();
        //controlMenu.SetActive(false);
    }

    public void OnBase_Click()
    {
        baseMenu.SetActive(true);
        baseBtn.GetComponent<Image>().sprite = btn_clicked;
        baseBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1,1,1);
        controlMenu.SetActive(false);
        controlBtn.GetComponent<Image>().sprite = btn_unclicked;
        controlBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(130f / 255f, 130f / 255f, 130f / 255f);
    }

    public void OnControl_Click()
    {
        baseMenu.SetActive(false);
        baseBtn.GetComponent<Image>().sprite = btn_unclicked;
        baseBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(130f / 255f, 130f / 255f, 130f / 255f);
        controlMenu.SetActive(true);
        controlBtn.GetComponent<Image>().sprite = btn_clicked;
        controlBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1);
    }
    
    //BASE
 
    public void OnVolumeChange()
    {
        AudioListener.volume = volumeSlider.value / 100.0f;
    }

    public void OnGraphicsLeft()
    {
        currentQualityIndex = (currentQualityIndex - 1 + qualityLevels.Length) % qualityLevels.Length;
        UpdateQualityText();
        // 设置画质逻辑...
    }
    
    public void OnGraphicsRight()
    {
        currentQualityIndex = (currentQualityIndex + 1) % qualityLevels.Length;
        UpdateQualityText();
        // 设置画质逻辑...
    }

    public void OnDisplayModeLeft()
    {
        currentDisplayModeIndex = (currentDisplayModeIndex - 1 + displayModes.Length) % displayModes.Length;
        UpdateDisplayModeText();
        // 设置显示模式逻辑...
    }

    public void OnDisplayModeRight()
    {
        currentDisplayModeIndex = (currentDisplayModeIndex + 1) % displayModes.Length;
        UpdateDisplayModeText();
        // 设置显示模式逻辑...
    }

    public void OnLanguageLeft()
    {
        currentLanguageIndex = (currentLanguageIndex - 1 + languages.Length) % languages.Length;
        UpdateLanguageText();
        // 设置语言逻辑...
    }

    public void OnLanguageRight()
    {
        currentLanguageIndex = (currentLanguageIndex + 1) % languages.Length;
        UpdateLanguageText();
        // 设置语言逻辑...
    }

    public void OnDifficultyLeft()
    {
        currentDifficultyIndex = (currentDifficultyIndex - 1 + difficulties.Length) % difficulties.Length;
        UpdateDifficultyText();
        // 设置难度逻辑...
    }

    public void OnDifficultyRight()
    {
        currentDifficultyIndex = (currentDifficultyIndex + 1) % difficulties.Length;
        UpdateDifficultyText();
        // 设置难度逻辑...
    }

    private void UpdateQualityText()
    {
        graphicsText.text = qualityLevels[currentQualityIndex];
    }

    private void UpdateDisplayModeText()
    {
        displayModeText.text = displayModes[currentDisplayModeIndex];
    }

    private void UpdateLanguageText()
    {
        languageText.text = languages[currentLanguageIndex];
    }

    private void UpdateDifficultyText()
    {
        difficultyText.text = difficulties[currentDifficultyIndex];
    }

    public void OnSaveSettings()
    {
        // 保存设置逻辑...
        Debug.Log("设置已保存");
    }

    public void OnReturn()
    {
        // 返回逻辑...
        Debug.Log("返回主菜单");
        OnBase_Click();
        gameObject.SetActive(false);
        //mainMenu.SetActive(true);
    }
    
    //CONTROL

    public void OnKeyboardBtn_Click()
    {
        moveLeftText.text = MoveLeftKeys[0];
        moveRightText.text = MoveRightKeys[0];
        jumpText.text = JumpKeys[0];
        dashText.text = DashKeys[0];
        doubleJumpText.text = DoubleJumpKeys[0];
        attackText.text = AttackKeys[0];
        pauseText.text = PauseKeys[0];
        keyboardBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1);
        controllerBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(130f / 255f, 130f / 255f, 130f / 255f);
    }

    public void OnControllerBtn_Click()
    {
        moveLeftText.text = MoveLeftKeys[1];
        moveRightText.text = MoveRightKeys[1];
        jumpText.text = JumpKeys[1];
        dashText.text = DashKeys[1];
        doubleJumpText.text = DoubleJumpKeys[1];
        attackText.text = AttackKeys[1];
        pauseText.text = PauseKeys[1];
        keyboardBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(130f / 255f, 130f / 255f, 130f / 255f);
        controllerBtn.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1);
    }

    public void OnDIYKey()
    {
        // 自定义按键逻辑...
        Debug.Log("自定义按键");
    }

    public void OnDefault()
    {
        
    }
}
