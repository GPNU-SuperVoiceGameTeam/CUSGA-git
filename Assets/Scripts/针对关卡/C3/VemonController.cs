using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VemonController : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰到玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(1);
            Destroy(gameObject); // 销毁毒液
        }

        // 如果碰到地面或其他物体
        if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject); // 销毁毒液
        }
        if(collision.CompareTag("highWave")){
            Destroy(collision);
            Destroy(gameObject);
        }
    }
}
