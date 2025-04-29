using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterflowPush : MonoBehaviour
{
    private bool isEnter;
    public Rigidbody2D player;
    public float pushForce = 5;

    void Update()
    {
        if (isEnter)
        {
            player.AddForce(new Vector2(pushForce, -pushForce)); // 调整力的方向为水平
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = false;
        }
    }
}
