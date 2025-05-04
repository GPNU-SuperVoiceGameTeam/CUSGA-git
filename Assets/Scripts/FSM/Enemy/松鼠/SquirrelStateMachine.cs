using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SquirrelStateMachine : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Attack
    }
    public Collider2D PlayerCheck;
    public State currentState;
    public float patrolSpeed = 2f; // 巡逻速度
    public float patrolRange = 5f; // 巡逻范围
    public float attackCooldown = 1f; // 攻击冷却时间，设置为每秒攻击一次
    public float shootForce = 10f;
    public GameObject acornPrefab; // 松果预制体
    public Transform acornSpawnPoint; // 松果发射点
    public Transform player; // 玩家引用

    private float nextAttackTime = 0f; // 下一次攻击的时间
    private Vector3 patrolLeftBoundary; // 巡逻左边界
    private Vector3 patrolRightBoundary; // 巡逻右边界
    private bool isMovingRight = true; // 是否向右移动

    void Start()
    {
        // 设置巡逻边界
        patrolLeftBoundary = (Vector2)transform.position + new Vector2(-patrolRange, 0);
        patrolRightBoundary = (Vector2)transform.position + new Vector2(patrolRange, 0);
    }

    void Update()
    {
        bool playerDetected = PlayerCheck.IsTouchingLayers(LayerMask.GetMask("Player"));

        if (playerDetected)
        {
            currentState = State.Attack;
            LookAtPlayer(); // 面向玩家
        }
        else
        {
            currentState = State.Patrol;
        }

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Attack:
                LaunchAcorn();
                break;
        }
    }

    void Patrol()
    {
        // 正常行走巡逻
        if (isMovingRight)
        {
            // 向右移动
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position = Vector2.MoveTowards(transform.position, patrolRightBoundary, patrolSpeed * Time.deltaTime);
            if (transform.position.x >= patrolRightBoundary.x)
            {
                isMovingRight = false; // 到达右边界，切换方向
            }
        }
        else
        {
            // 向左移动
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = Vector2.MoveTowards(transform.position, patrolLeftBoundary, patrolSpeed * Time.deltaTime);
            if (transform.position.x <= patrolLeftBoundary.x)
            {
                isMovingRight = true; // 到达左边界，切换方向
            }
        }
    }

    void LaunchAcorn()
    {
        // 检查是否达到攻击冷却时间
        if (Time.time >= nextAttackTime)
        {
            // 创建松果
            GameObject acorn = Instantiate(acornPrefab, acornSpawnPoint.position, Quaternion.identity);

            // 设置松果的旋转
            acorn.GetComponent<Rigidbody2D>().angularVelocity = 500f; // 设置松果的旋转速度

            // 计算朝向玩家的方向
            Vector2 direction = (player.position - acornSpawnPoint.position).normalized;

            // 给松果添加力使其朝向玩家
            Rigidbody2D acornRigidbody = acorn.GetComponent<Rigidbody2D>();
            acornRigidbody.gravityScale = 0f; // 取消重力
            if (acornRigidbody != null)
            {
                acornRigidbody.AddForce(direction * shootForce, ForceMode2D.Impulse);
            }

            // 更新下一次攻击时间
            nextAttackTime = Time.time + attackCooldown;
        }
    }
    void LookAtPlayer()
    {
        if (player == null) return;

        // 如果玩家在右边，松鼠向右；否则向左
        if (player.position.x > transform.position.x)
        {
            // 面向右
            transform.localScale = new Vector3(-1, 1, 1); // 根据你的设置可能需要调整
        }
        else
        {
            // 面向左
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
