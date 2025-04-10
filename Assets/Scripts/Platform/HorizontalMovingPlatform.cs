using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f; // 平台移动速度
    public float moveDistance = 2f; // 平台移动的距离
    public float startDelay = 0f; // 平台开始移动前的延迟时间
    private Vector3 startPosition;
    private float elapsedTime = 0f;
    private bool isDelaying = true;
    private bool movingRight = true;
    private Vector3 lastPlatformPosition;

    void Start()
    {
        startPosition = transform.position;
        lastPlatformPosition = transform.position;
    }

    void Update()
    {
        if (isDelaying)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= startDelay)
            {
                isDelaying = false;
                elapsedTime = 0f;
            }
            return;
        }

        float targetX;
        if (movingRight)
        {
            targetX = startPosition.x + moveDistance;
        }
        else
        {
            targetX = startPosition.x;
        }

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), step);

        if (Mathf.Abs(transform.position.x - targetX) < 0.001f)
        {
            movingRight = !movingRight;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lastPlatformPosition = transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 platformDelta = transform.position - lastPlatformPosition;
            if (platformDelta.magnitude > 0.0001f)
            {
                collision.transform.Translate(platformDelta.x, 0, 0);
            }
            lastPlatformPosition = transform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lastPlatformPosition = transform.position;
        }
    }
}