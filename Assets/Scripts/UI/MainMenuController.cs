using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

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
    public CanvasGroup canvasGroup; // 引用一个CanvasGroup组件
    public float fadeDuration = 1.0f; // 淡入淡出的持续时间

    private void Start()
    {
        Time.timeScale = 1.0f;
        settingsPanel.SetActive(false);
        fullScreenToggle.isOn = Screen.fullScreen;
        fullScreenToggle.onValueChanged.AddListener(SetFullscreenMode);
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
    }

    public void StartGame()
    {   
        AudioEventController.RaiseOnPlayAudio(AudioType.Click);
        
        // SceneManager.LoadScene("before_begin");
        StartCoroutine(FadeOutAndLoadScene("begin"));
    }

    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        AudioEventController.RaiseOnPlayAudio(AudioType.Click);
        float fadeSpeed = 1.0f / fadeDuration;
        float alpha = 0f;

        // 淡出过程
        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            canvasGroup.alpha = 1.0f - alpha;
            yield return null;
        }

        // 加载新场景
        SceneManager.LoadScene(sceneName);
    }

    // 打开/关闭设置面板
    public void OpenSettings()
    {
        AudioEventController.RaiseOnPlayAudio(AudioType.Click);
        if (settingsPanel != null){
            settingsPanel.SetActive(true);
        }           
    }

    public void ReturnToMainMenu()
    {
        AudioEventController.RaiseOnPlayAudio(AudioType.Click);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    // 退出游戏
    public void ExitGame()
    {
        AudioEventController.RaiseOnPlayAudio(AudioType.Click);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // 设置全屏模式
    public void SetFullscreenMode(bool isFullscreen)
{
    AudioEventController.RaiseOnPlayAudio(AudioType.Click);

    if (isFullscreen)
    {
        // 使用当前屏幕的原生分辨率进入全屏
        Resolution currentRes = Screen.currentResolution;
        Screen.SetResolution(currentRes.width, currentRes.height, FullScreenMode.ExclusiveFullScreen);
    }
    else
    {
        // 设置窗口大小为 1024x768，但不要强制锁定，让用户可以自由调整
        Screen.SetResolution(1024, 768, FullScreenMode.Windowed);
        
        // 可选：允许窗口可调整大小（仅限Editor或Standalone Windows平台）
        #if !UNITY_EDITOR && UNITY_STANDALONE_WIN
            // 注意：运行后无法通过API再更改窗口是否可调整大小，只能在Player Settings中设置
        #endif
    }
}
}