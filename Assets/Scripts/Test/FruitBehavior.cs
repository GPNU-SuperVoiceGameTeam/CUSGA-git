using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    public bool canPassThroughPlatforms = false;
    private Rigidbody2D rb;
    private PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // ���ˮ�����Դ���ƽ̨�����������ײ����߼�
        if (canPassThroughPlatforms)
        {
            // ʵ��ˮ�����Դ���ƽ̨���߼�
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ����ˮ����ƽ̨����ײ�߼�
        if (collision.gameObject.CompareTag("Platform") && !canPassThroughPlatforms)
        {
            Destroy(gameObject);
            // ˮ������ƽ̨��Ĵ������練��������
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.TakeDamage(1);

        }
    }
}
