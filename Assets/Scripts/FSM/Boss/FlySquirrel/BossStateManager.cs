using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BossState
{
    Airborne, // ����
    Ground, // ����
    Underground, // ����
    Attacking, // ����
    TakingDamage, // �ܻ�
    Dying // ����
}

public class BossStateManager : MonoBehaviour
{
    public LayerMask groundLayer; // ����㣬���ڼ���Ƿ��ڵ�����
    public Transform groundCheck; // �������
    public Vector2 groundCheckSize = new Vector2(1f, 0.1f); // �����ⷶΧ
    public float upGravity; // ��Ծʱ������С
    public float downGravity; // ����ʱ������С

    
    public BossState currentState; // ��ǰ״̬
    private BossStates bossStates; // ��ȡ����ֵϵͳ
    private Animator animator;
    private Rigidbody2D rb;
    private BossAttackManager attackManager; // ��ȡ����ϵͳ
    public Transform bossPosition; // bossλ��
    public Transform playerPosition; // ��ȡ���λ��
    #region �ɵ�����
    public float moveSpeed = 5f; // �ƶ��ٶ�
    public float slowDownDistance = 2f; // ��ʼ���ٵľ���
    public float stopDistance = 0.5f; // ֹͣ�ƶ��ľ���
    private float airTime = 5.0f; // ����ʱ��
    public float jumpForce = 10f; // ��Ծ����
    public float jumpInterval = 3.0f; // ��Ծ���
    #endregion
    public bool isGround;//�Ƿ����
    bool CanJump;
    float jumpTimer;
    bool StartLeafAttack = false;
    public float undergroundTimer; // ����״̬����ʱ��
    [Header("�޵�")]
    public bool invincible = true;
    public float invincibleInterval = 5.0f;
    public float invincibleTime;
    

    public Sprite flyLeaf;

    void Start()
    {
        currentState = BossState.Airborne; // ��ʼ��״̬
        bossStates = GetComponent<BossStates>();
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform; // ��ȡ��Ҷ����Transform���
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

    // boss����
    void FollowPlayerXAxis()
    {
        if (playerPosition != null)
        {
            float playerX = playerPosition.position.x;
            float bossX = transform.position.x;
            float distanceToPlayer = Mathf.Abs(playerX - bossX);

            
            float direction = Mathf.Sign(playerX - bossX);

            // ���������ҽ�Զ�������ƶ�
            if (distanceToPlayer > slowDownDistance)
            {
                MoveTowardsPlayer(direction, moveSpeed);
            }
            // ���������ҽϽ��������ƶ�
            else if (distanceToPlayer > stopDistance)
            {
                float slowSpeed = moveSpeed * (distanceToPlayer / slowDownDistance);
                MoveTowardsPlayer(direction, slowSpeed);
            }
            // ���������ҷǳ�����ֹͣ�ƶ�
            else
            {
                // ������������������߼������繥�����
            }

            // ����Ƿ񳬹����λ�ò�����
            if ((direction > 0 && bossX > playerX) || (direction < 0 && bossX < playerX))
            {
                // ����������λ�ã���ʼ���ٲ�����
                float returnSpeed = moveSpeed * 0.5f; // �����ٶ�
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

    void AirborneBehavior()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
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

    void GroundBehavior()
    {

        // ��������Լ��
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<BoxCollider2D>().isTrigger = false;

        // ����Ƿ��ڵ�����
        isGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);

        // ����Boss�ܵ��˺�
        bossStates.canTakeDamage = true;

        // ����Ƿ��ܵ�����
        if (bossStates.isHit && currentState == BossState.Ground)
        {
            bossStates.isHit = false;
            bossStates.TakeDamage(1); // �ܵ�1���˺�
            ChangeState(BossState.Underground); // �ܻ����л�������״̬
        }

        // ����Ѿ��ڵ����ϣ���ʼ��Ծ
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

    void UndergroundBehavior()
    {
        bossStates.canTakeDamage = false;
        // ��Boss��λ�����õ�����
        float undergroundY = -10f; // ���µ�Y��λ�ã����Ը���ʵ���������
        transform.position = new Vector2(transform.position.x, undergroundY);

        // ���÷�Ҷ����
        if (!StartLeafAttack && undergroundTimer <= 5f)
        {
            StartCoroutine(FlyingLeavesAttackRoutine());
            StartLeafAttack = true;
        } 
        undergroundTimer += Time.deltaTime;
        if(undergroundTimer >= 7f){
            attackManager.GiantFruitsAttack();
            transform.position = new Vector2(attackManager.playerX, 11f);
            currentState = BossState.Airborne;
        }
    }

    void AttackBehavior()
    {
        // ʵ�ֹ���״̬�߼�
        // ����Ƿ���Ҫ�л�������״̬
    }

    void TakeDamageBehavior()
    {
        // ʵ������״̬�߼�
        // ����Ƿ���Ҫ�л�������״̬
    }

    void DieBehavior()
    {
        // ʵ������״̬�߼�
        // ����Ƿ���Ҫ�л�������״̬
    }

    public void ChangeState(BossState newState)
    {
        currentState = newState;
    }

    // ��Ծ����
    void Jump()
    {
        float playerX = playerPosition.position.x;
        float bossX = transform.position.x;
        float direction = Mathf.Sign(playerX - bossX); // �������������ұߣ�Ϊ1������ߣ�Ϊ-1
        // ����ˮƽ���Ĵ�С
        float horizontalForce = jumpForce * 0.5f; // ���Ե�����������Կ���ˮƽ���Ĵ�С
        // �ϳ���Ծ��
        Vector2 jumpVector = new Vector2(direction * horizontalForce, jumpForce);
        // Ӧ����
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