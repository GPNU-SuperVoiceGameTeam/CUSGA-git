using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator transitionAnimator;
    public SceneManager sceneToLoad; // 场景名称
    public float transitionTime = 1f;
    public float longPressDuration = 2f; // 长按时间阈值

    private float pressStartTime = -1f; // 记录按下F键的时间
    private bool isLongPressing = false; // 是否正在长按F键

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            pressStartTime = Time.time;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            if (isLongPressing)
            {
                // SceneManager.LoadScene(sceneToLoad);
                isLongPressing = false;
            }
            pressStartTime = -1f;
        }

        if (pressStartTime != -1f && !isLongPressing)
        {
            if (Time.time - pressStartTime >= longPressDuration)
            {
                isLongPressing = true;
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
