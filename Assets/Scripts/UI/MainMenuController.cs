using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    // 引用三个按钮
    public Button startButton;
    public Button settingsButton;
    public Button exitButton;
    
    // 设置面板
    public GameObject settingsPanel;
    public Button returnButton;

    // 全屏选项
    public Toggle fullScreenToggle;

    // 淡入淡出效果相关
    public Image fadeImage; // 引用一个黑色的Image组件
    public float fadeDuration = 1.0f; // 淡入淡出的持续时间

    private void Start()
    {
        // 添加按钮点击事件监听器
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(ExitGame);
        returnButton.onClick.AddListener(ReturnToMainMenu);

        // 如果有设置面板，默认隐藏
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (fullScreenToggle != null)
        {
            fullScreenToggle.isOn = Screen.fullScreen;
            fullScreenToggle.onValueChanged.AddListener(SetFullscreenMode);
        }
        
        // 确保fadeImage初始透明度为0（完全透明）
        if(fadeImage != null)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
        }
    }

    // 开始游戏：加载游戏场景（例如 "GameScene"）
    private void StartGame()
    {   
        StartCoroutine(FadeOutAndLoadScene("before_begin"));
    }

    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float fadeSpeed = 1.0f / fadeDuration;
        float alpha = 0f;

        // 淡出过程
        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }

        // 加载新场景
        SceneManager.LoadScene(sceneName);
    }

    // 打开/关闭设置面板
    private void OpenSettings()
    {
        if (settingsPanel != null){
            settingsPanel.SetActive(true);
        }           
    }

    private void ReturnToMainMenu()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    // 退出游戏
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnDestroy()
    {
        // 移除事件监听器防止内存泄漏
        startButton.onClick.RemoveListener(StartGame);
        settingsButton.onClick.RemoveListener(OpenSettings);
        exitButton.onClick.RemoveListener(ExitGame);
    }

    // 设置全屏模式
    private void SetFullscreenMode(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (!isFullscreen)
        {
            Screen.SetResolution(1024, 768, false);
        }
    }
}