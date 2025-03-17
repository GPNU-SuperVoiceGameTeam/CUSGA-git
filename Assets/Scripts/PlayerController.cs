using System.Collections;
using System.Collections.Generic;
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

    [Header("״̬")]
    public bool isGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    public void Move()
    {
        // �������Ƿ��ڵ�����
        isGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        // ��ȡ����
        float moveInput = Input.GetAxis("Horizontal");
        // �ƶ����
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

    }

    public void Jump()
    {
        if (isGround && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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