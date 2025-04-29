using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; // 要加载的场景名称

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 加载指定的场景
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
