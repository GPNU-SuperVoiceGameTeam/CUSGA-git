using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueController : MonoBehaviour
{
    // public GameObject dialogue; // 对话框的引用
    // public PlayerController player; // 玩家控制器的引用
    // public bool isEnter; // 是否进入触发区域

    // void Update()
    // {
    //     if (isEnter)
    //     {
    //         // 检测按键输入
    //         if (Input.GetKeyDown(KeyCode.F))
    //         {
    //             // 如果按下 F 键，显示对话框
    //             showDialogue();
    //         }
    //         else if (Input.GetKeyDown(KeyCode.Z))
    //         {
    //             // 如果按下 Z 键，关闭对话框
    //             closeDialogue();
    //         }
    //     }
    // }

    // void OnTriggerStay2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //     {
    //         isEnter = true;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //     {
    //         isEnter = false;
    //     }
    // }

    // public void showDialogue()
    // {
    //     // 显示对话框
    //     dialogue.SetActive(true);
    //     player.canAttack = false;
    //     player.canMove = false;
    // }

    // public void closeDialogue()
    // {
    //     // 关闭对话框
    //     dialogue.SetActive(false);
    //     player.canAttack = true;
    //     player.canMove = true;
    // }
    public GameObject dialogue; // 对话框的引用
    public PlayerController player; // 玩家控制器的引用
    public Rigidbody2D playerRb; // 玩家的刚体组件
    public bool isEnter; // 是否进入触发区域
    private float fadeInTime = 0.5f; // 对话框淡入时间
    private CanvasGroup canvasGroup; // 对话框的 CanvasGroup 组件
    private bool isFadingIn = false; // 是否正在淡入
    private float fadeTimer = 0f; // 淡入计时器

    void Start()
    {
        // 初始化 CanvasGroup 组件
        canvasGroup = dialogue.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = dialogue.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f; // 初始透明度为 0
        dialogue.SetActive(true); // 确保对话框始终处于激活状态
    }

    void Update()
    {
        if (isEnter)
        {
            // 检测按键输入
            if (Input.GetKeyDown(KeyCode.F) && !isFadingIn)
            {
                // 开始淡入动画
                StartCoroutine(FadeInDialogue());
                playerRb.velocity = Vector2.zero; // 停止玩家的移动
            }
            else if (Input.GetKeyDown(KeyCode.C) && canvasGroup.alpha > 0f)
            {
                // 关闭对话框
                StartCoroutine(FadeOutDialogue());
            }
        }
    }

    IEnumerator FadeInDialogue()
    {
        isFadingIn = true; // 标记正在淡入
        player.canAttack = false;
        player.canMove = false;

        // 淡入动画
        fadeTimer = 0f;
        while (fadeTimer < fadeInTime)
        {
            fadeTimer += Time.deltaTime;
            canvasGroup.alpha = fadeTimer / fadeInTime; // 线性渐变透明度
            yield return null;
        }

        // 淡入完成
        canvasGroup.alpha = 1f; // 确保透明度为 1
        ShowText(); // 显示子物体（文字）
        isFadingIn = false;
    }

    IEnumerator FadeOutDialogue()
    {
        // 淡出动画
        fadeTimer = 0f;
        while (fadeTimer < fadeInTime)
        {
            fadeTimer += Time.deltaTime;
            canvasGroup.alpha = 1f - (fadeTimer / fadeInTime); // 线性渐变透明度
            yield return null;
        }

        // 淡出完成
        canvasGroup.alpha = 0f; // 确保透明度为 0
        HideText(); // 隐藏子物体（文字）
        player.canAttack = true;
        player.canMove = true;
    }

    void ShowText()
    {
        // 显示子物体（例如文字）
        foreach (Transform child in dialogue.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    void HideText()
    {
        // 隐藏子物体（例如文字）
        foreach (Transform child in dialogue.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = false;
        }
    }
}
