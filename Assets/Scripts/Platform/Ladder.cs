using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private float vertical;//垂直输入
    private float speed = 3f;//爬梯子速度
    private bool isLadder;//是否站在梯子上
    private bool isClimbing;//是否在爬梯子的状态
     private PlayerController playerController;
    [SerializeField] private Rigidbody2D rb;
   
    void Start()
    {
        playerController = rb.GetComponent<PlayerController>();
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;

        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = (rb.velocity.y > 0) ?
             playerController.upGravity :
             playerController.downGravity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
            isLadder = true;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}