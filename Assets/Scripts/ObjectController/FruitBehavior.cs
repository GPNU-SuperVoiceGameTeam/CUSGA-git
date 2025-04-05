using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    public bool canPassThroughPlatforms = false;
    private Rigidbody2D rb;
    private PlayerController player;
    private float destroyTime = 3f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        // 如果水果可以穿过平台，则调整其碰撞检测逻辑
        if (canPassThroughPlatforms)
        {
            // 实现水果可以穿过平台的逻辑
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 处理水果与平台的碰撞逻辑
        if (collision.gameObject.CompareTag("Platform") && !canPassThroughPlatforms)
        {
            Destroy(gameObject);
            // 水果碰到平台后的处理，例如反弹或销毁
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.TakeDamage(1);

        }
    }
    IEnumerator DestroyAfterTime()
    {
        // 等待三秒
        yield return new WaitForSeconds(destroyTime);
        // 销毁松果
        Destroy(gameObject);
    }
}
