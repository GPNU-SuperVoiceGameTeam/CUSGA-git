using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private float vertical; // 垂直输入
    public float speed = 3f; // 爬梯子速度
    private bool isLadder; // 是否站在梯子上
    private bool isClimbing; // 是否在攀爬状态
    private bool isHanging; // 是否处于挂梯状态
    private PlayerController playerController; // 玩家控制器脚本
    [SerializeField] private Rigidbody2D rb; // 玩家的 Rigidbody2D 组件

    void Start()
    {
        playerController = GetComponent<PlayerController>(); // 获取 PlayerController 脚本
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical"); // 获取垂直输入

        if (isLadder)
        {
            if (Mathf.Abs(vertical) > 0f)
            {
                isClimbing = true; // 如果有垂直输入，进入攀爬状态
                isHanging = false; // 退出挂梯状态
            }
            else if (Input.GetKeyDown(KeyCode.Space)) // 假设按下空格键进入挂梯状态
            {
                isHanging = !isHanging; // 切换挂梯状态
                isClimbing = false; // 退出攀爬状态
            }
        }
        else
        {
            isClimbing = false; // 如果不在梯子上，退出攀爬状态
            isHanging = false; // 退出挂梯状态
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f; // 爬梯子时关闭重力
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed); // 设置垂直速度
        }
        else if (isHanging)
        {
            rb.gravityScale = 0f; // 挂梯时关闭重力
            rb.velocity = Vector2.zero; // 停止所有运动
        }
        else
        {
            // 恢复默认重力
            rb.gravityScale = (rb.velocity.y > 0) ? playerController.upGravity : playerController.downGravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true; // 进入梯子区域
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false; // 离开梯子区域
            isClimbing = false; // 退出攀爬状态
            isHanging = false; // 退出挂梯状态
        }
    }
}