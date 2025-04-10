using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f; // ƽ̨�ƶ��ٶ�
    public float moveDistance = 2f; // ƽ̨�ƶ��ľ���
    public float startDelay = 0f; // ƽ̨��ʼ�ƶ�ǰ���ӳ�ʱ��
    public float pauseTimeAtEnds = 1f; // �ڶ���͵ײ���ͣ��ʱ��
    private Vector3 startPosition;
    private float elapsedTime = 0f;
    private bool isDelaying = true;
    private bool movingUp = true;
    private float pauseTimer = 0f;
    private bool isPaused = false;

    void Start()
    {
        startPosition = transform.position;
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

        if (isPaused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseTimeAtEnds)
            {
                isPaused = false;
                pauseTimer = 0f;
                movingUp = !movingUp;
            }
            return;
        }

        float targetY;
        if (movingUp)
        {
            targetY = startPosition.y + moveDistance;
        }
        else
        {
            targetY = startPosition.y;
        }

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), step);

        if (Mathf.Abs(transform.position.y - targetY) < 0.001f)
        {
            isPaused = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}    