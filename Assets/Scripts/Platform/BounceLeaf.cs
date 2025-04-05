using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceLeaf : MonoBehaviour
{
    public float bounceForce = 10f; // 弹力大小

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞的物体是否是玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse); // 给玩家施加弹力
        }
    }
}
