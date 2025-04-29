using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEgg : MonoBehaviour
{
    public int maxHits = 3; // 最大可承受的攻击次数
    public float timeout = 5.0f; // 超时时间
    public GameObject spiderPrefab; // 小蜘蛛的预制体
    private int hitCount = 0; // 当前被攻击的次数
    private bool isDestroyed = false; // 是否已经被打碎
    private float timer = 0.0f; // 计时器
    void Start()
    {
        
        spiderPrefab.SetActive(false); // 隐藏小蜘蛛预制体
    }
    void Update()
    {
        // 如果蜘蛛卵已经被打碎，直接返回
        if (isDestroyed) return;

        // 每帧更新计时器
        timer += Time.deltaTime;

        // 如果计时器超过超时时间
        if (timer >= timeout)
        {
            // 自动破碎并生成小蜘蛛
            DestroyEgg();
            spiderPrefab.SetActive(true);
            Instantiate(spiderPrefab, transform.position, Quaternion.identity);
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果蜘蛛卵已经被打碎，直接返回
        if (isDestroyed) return;

        // 检测是否是玩家攻击
        if (collision.gameObject.CompareTag("highWave"))
        {
            // 增加攻击次数
            hitCount++;
            Destroy(collision.gameObject);
            // 检查是否达到最大攻击次数
            if (hitCount >= maxHits)
            {
                // 打碎蜘蛛卵
                DestroyEgg();
            }
        }
    }

    void DestroyEgg()
    {
        // 标记蜘蛛卵已经被打碎
        isDestroyed = true;

        // 播放破碎动画或音效（如果有）
        // ...
        // 销毁蜘蛛卵对象
        Destroy(gameObject);
    }
}
