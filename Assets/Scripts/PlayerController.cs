using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    //地面检测
    public LayerMask groundLayer; // 地面层，用于检测是否在地面上
    public Transform groundCheck; // 地面检测点
    public Vector2 groundCheckSize = new Vector2(1f, 0.1f);//地面检测范围
    [Header("属性")]
    public float moveSpeed = 8f; // 移动速度
    public float jumpForce = 16f; // 跳跃力度

    public float upGravity;//跳跃时重力大小
    public float downGravity;//下落时重力大小

    [Header("状态")]
    public bool isGround;

    [Header("主角行动状态")]
    private string state_type = "isMoveAble";//isMoveAble,cantMoveAble

    #region 以下为私有属性
    private SpriteRenderer spriteRenderer;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {


        switch (state_type)
        {
            case "isMoveAble":
                Move();
                Jump();
                break;
            case "cantMoveAble":
                break;
        }

    }

    public void Move()
    {
        // 检测玩家是否在地面上
        isGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        // 获取输入
        float moveInput = Input.GetAxis("Horizontal");
        // 移动玩家
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput < 0) //根据输入翻转玩家
        {
           spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    public void Jump()
    {
        if (isGround && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        //根据下降改变重力
        
        if(rb.velocity.y>0.1f)
        {
            rb.gravityScale = upGravity;
        }
        else if(rb.velocity.y < -0.1f)//&&!isfloating)
        {
            rb.gravityScale = downGravity;
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(groundCheck.position, groundCheckSize);
        }
    }
}