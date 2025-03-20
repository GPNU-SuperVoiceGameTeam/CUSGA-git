using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    //������
    public LayerMask groundLayer; // ����㣬���ڼ���Ƿ��ڵ�����
    public Transform groundCheck; // �������
    public Vector2 groundCheckSize = new Vector2(1f, 0.1f);//�����ⷶΧ
    [Header("����")]
    public float moveSpeed = 8f; // �ƶ��ٶ�
    public float jumpForce = 16f; // ��Ծ����

    public float upGravity;//��Ծʱ������С
    public float downGravity;//����ʱ������С

    [Header("״̬")]
    public bool isGround;

    [Header("�����ж�״̬")]
    private string state_type = "isMoveAble";//isMoveAble,cantMoveAble

    #region ����Ϊ˽������
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
        // �������Ƿ��ڵ�����
        isGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        // ��ȡ����
        float moveInput = Input.GetAxis("Horizontal");
        // �ƶ����
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput < 0) //�������뷭ת���
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


        //�����½��ı�����
        
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