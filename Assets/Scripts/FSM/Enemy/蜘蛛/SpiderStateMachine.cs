using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiderStateMachine : MonoBehaviour
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
    public Transform player; // 玩家引用
    private bool isMovingRight = true; // 是否向右移动

    public Transform patrolLeft;// 巡逻左边界
    public Transform patrolRight;// 巡逻右边界

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                patrolSpeed = 2;
                Patrol();
                break;
            case State.Attack:
                patrolSpeed = 4;
                Patrol();
                break;
        }
    }

    void Patrol()
    {
        // 正常行走巡逻
        if (isMovingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            // 向右移动
            transform.position = Vector2.MoveTowards(transform.position, patrolRight.position, patrolSpeed * Time.deltaTime);
            if (transform.position.x >= patrolRight.position.x)
            {
                isMovingRight = false; // 到达右边界，切换方向
            }
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            // 向左移动
            transform.position = Vector2.MoveTowards(transform.position, patrolLeft.position, patrolSpeed * Time.deltaTime);
            if (transform.position.x <= patrolLeft.position.x)
            {
                isMovingRight = true; // 到达左边界，切换方向
            }
        }
    }
}
