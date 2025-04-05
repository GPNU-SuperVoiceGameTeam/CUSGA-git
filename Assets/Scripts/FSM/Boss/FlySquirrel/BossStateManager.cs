using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BossState
{
    Airborne, // 空中
    Ground, // 地面
    Underground, // 地下
    Attacking, // 攻击
    TakingDamage, // 受击
    Dying // 死亡
}

public class BossStateManager : MonoBehaviour
{
    public LayerMask groundLayer; // 地面层，用于检测是否在地面上
    public Transform groundCheck; // 地面检测点
    public Vector2 groundCheckSize = new Vector2(1f, 0.1f); // 地面检测范围
    public float upGravity; // 跳跃时重力大小
    public float downGravity; // 下落时重力大小

    
    public BossState currentState; // 当前状态
    private Health bossStates; // 获取生命值系统
    private Animator animator;
    private Rigidbody2D rb;
    private BossAttackManager attackManager; // 获取攻击系统
    public Transform bossPosition; // boss位置
    public Transform playerPosition; // 获取玩家位置
    #region 可调参数
    public float moveSpeed = 5f; // 移动速度
    public float slowDownDistance = 2f; // 开始减速的距离
    public float stopDistance = 0.5f; // 停止移动的距离
    private float airTime = 5.0f; // 空中时间
    public float jumpForce = 10f; // 跳跃力度
    public float jumpInterval = 3.0f; // 跳跃间隔
    #endregion
    public bool isGround;//是否地面
    bool CanJump;
    float jumpTimer;
    bool StartLeafAttack = false;
    public float undergroundTimer; // 地下状态持续时间
    [Header("无敌")]
    public bool invincible = true;
    public float invincibleInterval = 5.0f;
    public float invincibleTime;
    

    public Sprite flyLeaf;

    void Start()
    {
        currentState = BossState.Airborne; // 初始化状态
        bossStates = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform; // 获取玩家对象的Transform组件
        attackManager = GetComponent<BossAttackManager>();

    }

    void Update()
    {
        if(bossStates.currentHP > 0){
            switch (currentState)
            {
                case BossState.Airborne:
                    AirborneBehavior();
                    FollowPlayerXAxis();
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
        }
        
    }

    // boss跟随
    void FollowPlayerXAxis()
    {
        if (playerPosition != null)
        {
            float playerX = playerPosition.position.x;
            float bossX = transform.position.x;
            float distanceToPlayer = Mathf.Abs(playerX - bossX);

            
            float direction = Mathf.Sign(playerX - bossX);

            // 如果距离玩家较远，正常移动
            if (distanceToPlayer > slowDownDistance)
            {
                MoveTowardsPlayer(direction, moveSpeed);
            }
            // 如果距离玩家较近，减速移动
            else if (distanceToPlayer > stopDistance)
            {
                float slowSpeed = moveSpeed * (distanceToPlayer / slowDownDistance);
                MoveTowardsPlayer(direction, slowSpeed);
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
                MoveTowardsPlayer(-direction, returnSpeed);
            }
        }
    }

    void MoveTowardsPlayer(float direction, float speed)
    {
        transform.position = new Vector3(
            transform.position.x + direction * speed * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
    }
    #region 空中
    void AirborneBehavior()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        bossPosition = transform;
        attackManager.FallingFruitsAttack();
        airTime -= Time.deltaTime;
        if (airTime <= 0)
            attackManager.timeBetweenFruits = 1f;
        if (bossStates.isHit && currentState == BossState.Airborne)
        {
            ChangeState(BossState.Ground);
            bossStates.isHit = false;
        }
    }
    #endregion
    #region 地面
    void GroundBehavior()
    {

        // 设置物理约束
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<BoxCollider2D>().isTrigger = false;

        // 检测是否在地面上
        isGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);

        // 允许Boss受到伤害
        bossStates.canTakeDamage = true;

        // 检查是否受到攻击
        if (bossStates.isHit && currentState == BossState.Ground)
        {
            bossStates.isHit = false;
            bossStates.TakeDamage(1); // 受到1点伤害
            ChangeState(BossState.Underground); // 受击后切换到地下状态
        }

        // 如果已经在地面上，开始跳跃
        if (isGround&&CanJump)
        {
            //StartCoroutine(JumpRoutine());
            //InvokeRepeating("Jump", 0f, jumpInterval);
            Jump();
            CanJump = false;
        }

        if (!CanJump)
        {
            jumpTimer +=Time.deltaTime;
            if   (jumpTimer>=jumpInterval)
            {
                CanJump = true;
                jumpTimer = 0;
            }


        }
    }
    #endregion
    #region 地下
    void UndergroundBehavior()
    {
        bossStates.canTakeDamage = false;
        // 将Boss的位置设置到地下
        float undergroundY = -10f; // 地下的Y轴位置，可以根据实际需求调整
        transform.position = new Vector2(transform.position.x, undergroundY);

        // 启用飞叶技能
        if (!StartLeafAttack && undergroundTimer <= 5f)
        {
            StartCoroutine(FlyingLeavesAttackRoutine());
            StartLeafAttack = true;
        } 
        undergroundTimer += Time.deltaTime;
        if(undergroundTimer >= 7f){
            attackManager.GiantFruitsAttack();
            transform.position = new Vector2(attackManager.playerX, 10.5f);
            
            undergroundTimer = 0f;
            StartLeafAttack = false;
            airTime = 5.0f; // 重置空中时间
            currentState = BossState.Airborne;
        }
    }
     #endregion

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

    // 跳跃方法
    void Jump()
    {
        float playerX = playerPosition.position.x;
        float bossX = transform.position.x;
        float direction = Mathf.Sign(playerX - bossX); // 方向：如果玩家在右边，为1；在左边，为-1
        // 计算水平力的大小
        float horizontalForce = jumpForce * 0.5f; // 可以调整这个比例以控制水平力的大小
        // 合成跳跃力
        Vector2 jumpVector = new Vector2(direction * horizontalForce, jumpForce);
        // 应用力
        rb.AddForce(jumpVector, ForceMode2D.Impulse);
        if (rb.velocity.y > 0.1f)
        {
            rb.gravityScale = upGravity;
        }
        else if (rb.velocity.y < -0.1f)
        {
            rb.gravityScale = downGravity;
        }
    }

    //IEnumerator JumpRoutine()
    //{
    //    while (currentState == BossState.Ground)
    //    {
    //        Jump();
    //        yield return new WaitForSeconds(jumpInterval);
    //    }
    //}

    IEnumerator FlyingLeavesAttackRoutine()
    {
        while (currentState == BossState.Underground)
        {
            attackManager.FlyingLeavesAttack();
            yield return new WaitForSeconds(2.0f);
        }
    }
}