using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapChange : MonoBehaviour
{
    public string sceneTo;
    private bool isReady;
    public Image fadeImage; // 引用一个黑色的Image组件
    public float fadeDuration = 1.0f; // 淡入淡出的持续时间

    private float fadeTimer;
    private bool isFading = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isReady)
        {
            StartCoroutine(FadeOut()); // 开始淡出
        }

        if (isFading)
        {
            fadeTimer += Time.deltaTime;

            if (fadeTimer >= fadeDuration)
            {
                fadeTimer = fadeDuration;
                isFading = false;
                SceneManager.LoadScene(sceneTo); // 加载新场景
            }

            float alpha = fadeTimer / fadeDuration;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isReady = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isReady = false;
        }
    }

    IEnumerator FadeOut()
    {
        isFading = true;
        fadeTimer = 0;
        while (fadeTimer < fadeDuration)
        {
            yield return null; // 等待下一帧
        }
    }
}