using UnityEngine;

public class BookFlip : MonoBehaviour
{
    // 翻转的目标角度
    public float flipAngle = 90f;
    // 翻转的速度
    public float flipSpeed = 5f;
    // 返回的速度
    public float returnSpeed = 3f;

    private bool isFlipped = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    void Start()
    {
        // 记录初始旋转
        initialRotation = transform.rotation;
        // 计算翻转后的目标旋转
        targetRotation = Quaternion.Euler(0, 0, flipAngle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 当玩家进入触发器时，开始翻转
        if (other.CompareTag("Player"))
        {
            isFlipped = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 当玩家离开触发器时，开始返回
        if (other.CompareTag("Player"))
        {
            isFlipped = false;
        }
    }

    void Update()
    {
        if (isFlipped)
        {
            // 翻转到目标角度
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, flipSpeed * Time.deltaTime);
        }
        else
        {
            // 返回初始角度
            transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, returnSpeed * Time.deltaTime);
        }
    }
}