// 需要添加的命名空间
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControlerInstance : MonoBehaviour
{
    public static VolumeControlerInstance Instance;
    private Slider volumeSlider;
    public float curVolume = 0.5f;

    void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // 监听场景加载事件
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 初始化音量（从 PlayerPrefs 读取）
        curVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 每次场景加载时尝试绑定 Slider
        BindSlider();
    }

    void BindSlider()
    {
        // 更高效的查找方式（包含隐藏对象）
        Slider foundSlider = FindObjectOfType<Slider>(true);
        
        if (foundSlider != null && foundSlider.name == "VolumeSlider")
        {
            // 解除旧监听器（如果存在）
            if (volumeSlider != null)
            {
                volumeSlider.onValueChanged.RemoveListener(HandleVolumeChanged);
            }

            volumeSlider = foundSlider;
            volumeSlider.value = curVolume; // 同步当前音量值
            
            // 绑定新监听器
            volumeSlider.onValueChanged.AddListener(HandleVolumeChanged);
        }
    }

    void HandleVolumeChanged(float newVolume)
    {
        curVolume = newVolume;
        PlayerPrefs.SetFloat("MasterVolume", curVolume);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            if (volumeSlider != null)
            {
                volumeSlider.onValueChanged.RemoveListener(HandleVolumeChanged);
            }
        }
    }
}