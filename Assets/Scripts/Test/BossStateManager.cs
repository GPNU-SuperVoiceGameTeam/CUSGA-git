using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BossState
{
    Airborne,
    Ground,
    Underground,
    Attacking,
    TakingDamage,
    Dying
}
public class BossStateManager : MonoBehaviour
{
    public BossState currentState;//当前状态
    private BossStates bossStates;//获取生命值系统
    private Animator animator;
    private BossAttackManager attackManager;//获取攻击系统
    public Transform bossPosition;//boss位置
    public Transform playerPosition;//获取玩家位置
    public float moveSpeed = 5f;//移动速度
    public float slowDownDistance = 2f; // 开始减速的距离
    public float stopDistance = 0.5f; // 停止移动的距离

    void Start()
    {
        currentState = BossState.Airborne;//初始化状态
        //bossStates = GetComponent<BossStates>();
        //animator = GetComponent<Animator>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform; // 获取玩家对象的Transform组件
        attackManager = GetComponent<BossAttackManager>();
    }

    void Update()
    {
        switch (currentState)
        {
            case BossState.Airborne:
                AirborneBehavior();
                break;
            case BossState.Ground:
                GroundBehavior();
                break;
            case BossState.Underground:
                UndergroundBehavior();
                break;
            case BossState.Attacking:
                AttackBehavior();
                break;
            case BossState.TakingDamage:
                TakeDamageBehavior();
                break;
            case BossState.Dying:
                DieBehavior();
                break;
        }

        FollowPlayerXAxis();
    }
    //boss跟随
    void FollowPlayerXAxis()
    {
        if (playerPosition != null)
        {
            float playerX = playerPosition.position.x;
            float bossX = transform.position.x;
            float distanceToPlayer = Mathf.Abs(playerX - bossX);

            // 计算移动方向
            float direction = Mathf.Sign(playerX - bossX);

            // 如果距离玩家较远，正常移动
            if (distanceToPlayer > slowDownDistance)
            {
                transform.position = new Vector3(
                    transform.position.x + direction * moveSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );
            }
            // 如果距离玩家较近，减速移动
            else if (distanceToPlayer > stopDistance)
            {
                float slowSpeed = moveSpeed * (distanceToPlayer / slowDownDistance);
                transform.position = new Vector3(
                    transform.position.x + direction * slowSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );
            }
            // 如果距离玩家非常近，停止移动
            else
            {
                // 可以在这里添加其他逻辑，比如攻击玩家
            }

            // 检查是否超过玩家位置并调整
            if ((direction > 0 && bossX > playerX) || (direction < 0 && bossX < playerX))
            {
                // 如果超过玩家位置，开始减速并返回
                float returnSpeed = moveSpeed * 0.5f; // 返回速度
                transform.position = new Vector3(
                    transform.position.x - direction * returnSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );
            }
        }
    }
    void AirborneBehavior()
    {
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        bossPosition = this.transform;
        attackManager.FallingFruitsAttack();
        // 实现空中状态逻辑
        // 检查是否需要切换到其他状态
    }

    void GroundBehavior()
    {
        // 实现地面状态逻辑
        // 检查是否需要切换到其他状态
    }

    void UndergroundBehavior()
    {
        // 实现地下状态逻辑
        // 检查是否需要切换到其他状态
    }

    void AttackBehavior()
    {
        // 实现攻击状态逻辑
        // 检查是否需要切换到其他状态
    }

    void TakeDamageBehavior()
    {
        // 实现受伤状态逻辑
        // 检查是否需要切换到其他状态
    }

    void DieBehavior()
    {
        // 实现死亡状态逻辑
        // 检查是否需要切换到其他状态
    }

    public void ChangeState(BossState newState)
    {
        currentState = newState;
    }

    //状态转换逻辑
    //void CheckStateTransitions()
    //{
    //    if (currentState == BossState.Airborne)
    //    {
    //        if (shouldTransitionToGround)
    //        {
    //            currentState = BossState.Ground;
    //        }
    //        else if (shouldTransitionToUnderground)
    //        {
    //            currentState = BossState.Underground;
    //        }
    //    }
    //    // 其他状态转换条件...
    //}
}
