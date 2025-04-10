using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public float breakDelay = 0f; // 破碎延迟时间，单位：秒

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 调用破碎方法，可设置延迟
            Invoke("BreakPlatform", breakDelay);
        }
    }

    void BreakPlatform()
    {
        // 这里可以添加破碎动画或音效
        Destroy(gameObject);
    }
}