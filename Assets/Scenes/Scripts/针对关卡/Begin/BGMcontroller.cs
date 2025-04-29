using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMcontroller : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject); // 确保对象在场景切换时不会被销毁

        // 注册场景加载事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 如果加载的场景名称是 "C01"，销毁当前 GameObject
        if (scene.name == "C01")
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // 移除事件监听，防止内存泄漏
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}