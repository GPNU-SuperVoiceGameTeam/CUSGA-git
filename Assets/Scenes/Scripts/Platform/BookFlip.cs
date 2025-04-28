using UnityEngine;

public class BookFlip : MonoBehaviour
{
    // ��ת��Ŀ��Ƕ�
    public float flipAngle = 90f;
    // ��ת���ٶ�
    public float flipSpeed = 5f;
    // ���ص��ٶ�
    public float returnSpeed = 3f;

    private bool isFlipped = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    void Start()
    {
        // ��¼��ʼ��ת
        initialRotation = transform.rotation;
        // ���㷭ת���Ŀ����ת
        targetRotation = Quaternion.Euler(0, 0, flipAngle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ����ҽ��봥����ʱ����ʼ��ת
        if (other.CompareTag("Player"))
        {
            isFlipped = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ������뿪������ʱ����ʼ����
        if (other.CompareTag("Player"))
        {
            isFlipped = false;
        }
    }

    void Update()
    {
        if (isFlipped)
        {
            // ��ת��Ŀ��Ƕ�
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, flipSpeed * Time.deltaTime);
        }
        else
        {
            // ���س�ʼ�Ƕ�
            transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, returnSpeed * Time.deltaTime);
        }
    }
}