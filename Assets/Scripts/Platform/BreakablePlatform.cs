using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public float breakDelay = 0.2f; // 平台塌陷的延迟时间
    public float restoreDelay = 2.0f; // 平台恢复的延迟时间

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("BreakPlatform", breakDelay); // 延迟调用塌陷方法
        }
    }

    void BreakPlatform()
    {
        gameObject.SetActive(false); // 塌陷平台
        Invoke("RestorePlatform", restoreDelay); // 延迟调用恢复平台的方法
    }

    void RestorePlatform()
    {
        gameObject.SetActive(true); // 恢复平台
    }
}