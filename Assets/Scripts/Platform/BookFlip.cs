using UnityEngine;

public class BookFlip : MonoBehaviour
{
    // 翻转的目标角度（正负表示左右）
    public float flipAngle = 90f;
    // 翻转的速度
    public float flipSpeed = 5f;
    // 返回的速度
    public float returnSpeed = 3f;

    private bool isFlipped = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private Transform player;

    void Start()
    {
        initialRotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFlipped = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFlipped = false;
        }
    }

    void Update()
    {   
        float direction = GetFlipDirection(player.transform.position);
        targetRotation = Quaternion.Euler(0, 0, direction * flipAngle);
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

    // 根据玩家位置判断翻转方向（左/右）
    private float GetFlipDirection(Vector3 playerPosition)
    {
        // 判断玩家是在书本的左边还是右边
        if (playerPosition.x < transform.position.x)
        {
            return 1f; // 向左翻转
        }
        else
        {
            return -1f; // 向右翻转
        }
    }
}