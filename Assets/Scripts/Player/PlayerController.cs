using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    
    //������
    public LayerMask groundLayer; // ����㣬���ڼ���Ƿ��ڵ�����
    public Transform groundCheck; // �������
    public Vector2 groundCheckSize = new Vector2(1f, 0.1f);//�����ⷶΧ
    //����Ԥ��
    public GameObject bulletPrefab;//����Ԥ����
    public float shootForce = 5.0f;//�������
    public float keepTime = 2.0f;//��������ʱ��

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
    private Rigidbody2D rb;
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
                Shoot();
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
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //��ȡ�������
            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
            Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            //ʵ��������
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BattleWave battleWave = bullet.GetComponent<BattleWave>();
            battleWave.Initialize(keepTime);
            //��������
            Vector2 direction = (worldPosition - position2D).normalized;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            //��������
            rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
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