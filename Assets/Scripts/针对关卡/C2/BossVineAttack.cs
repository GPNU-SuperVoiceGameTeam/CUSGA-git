using UnityEngine;

public class BossVineAttack : MonoBehaviour
{
    private PolygonCollider2D vinecollider2D;
    private float timer = 0f;

    [SerializeField] private float preDelay = 1f;     // 等待时间开始伸长
    [SerializeField] private float extendDuration = 1f; // 伸长时间
    [SerializeField] private float activeDuration = 1f; // 活动持续时间
    [SerializeField] private float retractDuration = 0.5f; // 缩回时间

    private Vector3 initialScale;
    private Vector3 targetExtendScale = new Vector3(1, -1, 1);
    private Vector3 targetRetractScale = new Vector3(1, -0.3f, 1);

    private bool isExtending = false;
    private bool isRetracting = false;

    void Awake()
    {
        vinecollider2D = GetComponent<PolygonCollider2D>();
        initialScale = transform.localScale;
        transform.localScale = targetRetractScale; // 初始是缩回状态
    }

    public void Initialize()
    {
        vinecollider2D.isTrigger = true; // 初始无伤害
        StartCoroutine(ManageVine());
    }

    System.Collections.IEnumerator ManageVine()
    {
        // Step 1: 等待一秒不触发
        yield return new WaitForSeconds(preDelay);

        // Step 2: 伸长动画（1秒内完成）
        float t = 0f;
        while (t < extendDuration)
        {
            t += Time.deltaTime;
            float normalized = t / extendDuration;
            transform.localScale = Vector3.Lerp(targetRetractScale, targetExtendScale, normalized);
            yield return null;
        }
        transform.localScale = targetExtendScale;

        // Step 3: 开启碰撞检测，有伤害
        vinecollider2D.isTrigger = false;

        // Step 4: 持续1秒
        yield return new WaitForSeconds(activeDuration);

        // Step 5: 缩回动画（0.5秒）
        t = 0f;
        while (t < retractDuration)
        {
            t += Time.deltaTime;
            float normalized = t / retractDuration;
            transform.localScale = Vector3.Lerp(targetExtendScale, targetRetractScale, normalized);
            yield return null;
        }
        transform.localScale = targetRetractScale;

        // Step 6: 关闭碰撞，再销毁
        vinecollider2D.isTrigger = true;
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(1);
        }
    }
}