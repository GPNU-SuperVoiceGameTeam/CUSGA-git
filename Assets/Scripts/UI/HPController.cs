using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    public Sprite[] hpSprites; // 血量图标数组
    public PlayerController playerController; // 玩家控制器引用
    public int hp; // 当前血量
    private Image image; // 缓存的SpriteRenderer组件

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>(); // 缓存SpriteRenderer组件
        hp = playerController.health; // 初始化血量
        UpdateHP(); // 更新血量图标和颜色
    }

    // Update is called once per frame
    void Update()
    {
        // 检测血量是否发生变化
        if (hp != playerController.health)
        {
            hp = playerController.health; // 更新血量
            UpdateHP(); // 更新血量图标和颜色
            StartCoroutine(FillAnimation()); // 播放填充动画
        }
    }

    // 更新血量图标和颜色
    private void UpdateHP()
    {
        if (hp < 0) hp = 0;
        image.sprite = hpSprites[hp - 1]; // 更新血量图标
        SetColor(); // 设置颜色
    }

    // 根据血量设置颜色
    public void SetColor()
    {
        switch (hp)
        {
            case 5:
            case 4:
                image.color = new Color(43f / 255f, 212f / 255f, 0f / 255f, 255f / 255f); // 绿色
                break;
            case 3:
            case 2:
                image.color = new Color(212f / 255f, 159f / 255f, 0f / 255f, 255f / 255f); // 橙色
                break;
            case 1:
                image.color = Color.red; // 红色
                break;
            default:
                break;
        }
        
    }
    // 播放填充动画的协程
    private IEnumerator FillAnimation()
    {
        // 从0到1的填充动画，持续0.5秒
        float elapsedTime = 0f;
        float duration = 0.5f;
        image.fillAmount = 0f; // 从0开始
        while (elapsedTime < duration)
        {
            image.fillAmount = elapsedTime / duration; // 线性插值
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.fillAmount = 1f; // 确保填充完成
    }
}