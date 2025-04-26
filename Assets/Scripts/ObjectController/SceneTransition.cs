using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; // Ҫ���صĳ�������

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ����ָ���ĳ���
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
