using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public float breakDelay = 0f; // �����ӳ�ʱ�䣬��λ����

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �������鷽�����������ӳ�
            Invoke("BreakPlatform", breakDelay);
        }
    }

    void BreakPlatform()
    {
        // �������������鶯������Ч
        Destroy(gameObject);
    }
}