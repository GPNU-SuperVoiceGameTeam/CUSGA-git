//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//public enum BossState
//{
//    Airborne,//����
//    Ground,//����
//    Underground,//����
//    Attacking,//����
//    TakingDamage,//�ܻ�
//    Dying//����
//}
//public class BossStateManager : MonoBehaviour
//{
//    public LayerMask groundLayer; // ����㣬���ڼ���Ƿ��ڵ�����
//    public Transform groundCheck; // �������
//    public Vector2 groundCheckSize = new Vector2(1f, 0.1f);//�����ⷶΧ
//    public float upGravity;//��Ծʱ������С
//    public float downGravity;//����ʱ������С

//    public BossState currentState;//��ǰ״̬
//    private BossStates bossStates;//��ȡ����ֵϵͳ
//    private Animator animator;
//    private Rigidbody2D rb;
//    private BossAttackManager attackManager;//��ȡ����ϵͳ
//    public Transform bossPosition;//bossλ��
//    public Transform playerPosition;//��ȡ���λ��
//    public float moveSpeed = 5f;//�ƶ��ٶ�
//    public float slowDownDistance = 2f; // ��ʼ���ٵľ���
//    public float stopDistance = 0.5f; // ֹͣ�ƶ��ľ���
//    private float airTime = 5.0f;//����ʱ��
//    public float jumpForce = 10f;//��Ծ����
//    public float jumpInterval = 2.0f;//��Ծ���
//    public bool isGround;

//    public Sprite flyLeaf;

//    void Start()
//    {
//        currentState = BossState.Airborne;//��ʼ��״̬
//        bossStates = GetComponent<BossStates>();
//        rb = GetComponent<Rigidbody2D>();
//        //animator = GetComponent<Animator>();
//        playerPosition = GameObject.FindGameObjectWithTag("Player").transform; // ��ȡ��Ҷ����Transform���
//        attackManager = GetComponent<BossAttackManager>();
//    }

//    void Update()
//    {
//        switch (currentState)
//        {
//            case BossState.Airborne:
//                AirborneBehavior();
//                FollowPlayerXAxis();
//                break;
//            case BossState.Ground:
//                GroundBehavior();
//                break;
//            case BossState.Underground:
//                UndergroundBehavior();
//                break;
//            case BossState.Attacking:
//                AttackBehavior();
//                break;
//            case BossState.TakingDamage:
//                TakeDamageBehavior();
//                break;
//            case BossState.Dying:
//                DieBehavior();
//                break;
//        }


//    }
//    //boss����
//    void FollowPlayerXAxis()
//    {
//        if (playerPosition != null)
//        {
//            float playerX = playerPosition.position.x;
//            float bossX = transform.position.x;
//            float distanceToPlayer = Mathf.Abs(playerX - bossX);

//            // �����ƶ�����
//            float direction = Mathf.Sign(playerX - bossX);

//            // ���������ҽ�Զ�������ƶ�
//            if (distanceToPlayer > slowDownDistance)
//            {
//                transform.position = new Vector3(
//                    transform.position.x + direction * moveSpeed * Time.deltaTime,
//                    transform.position.y,
//                    transform.position.z
//                );
//            }
//            // ���������ҽϽ��������ƶ�
//            else if (distanceToPlayer > stopDistance)
//            {
//                float slowSpeed = moveSpeed * (distanceToPlayer / slowDownDistance);
//                transform.position = new Vector3(
//                    transform.position.x + direction * slowSpeed * Time.deltaTime,
//                    transform.position.y,
//                    transform.position.z
//                );
//            }
//            // ���������ҷǳ�����ֹͣ�ƶ�
//            else
//            {
//                // ������������������߼������繥�����
//            }

//            // ����Ƿ񳬹����λ�ò�����
//            if ((direction > 0 && bossX > playerX) || (direction < 0 && bossX < playerX))
//            {
//                // ����������λ�ã���ʼ���ٲ�����
//                float returnSpeed = moveSpeed * 0.5f; // �����ٶ�
//                transform.position = new Vector3(
//                    transform.position.x - direction * returnSpeed * Time.deltaTime,
//                    transform.position.y,
//                    transform.position.z
//                );
//            }
//        }
//    }
//    void AirborneBehavior()
//    {

//        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
//        bossPosition = this.transform;
//        attackManager.FallingFruitsAttack();
//        airTime -= Time.deltaTime;
//        if(airTime <= 0)
//            attackManager.timeBetweenFruits = 1f;
//        if (bossStates.isHit && currentState == BossState.Airborne)
//        {
//            currentState = BossState.Ground;
//            bossStates.isHit = false;
//        }

//    }

//    void GroundBehavior()
//    {
//        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
//        this.GetComponent<BoxCollider2D>().isTrigger = false;
//        isGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
//        bossStates.canTakeDamage = true;
//        if (bossStates.isHit && currentState == BossState.Ground)
//        {
//            bossStates.isHit = false;
//            currentState = BossState.Underground;
//        }
//        InvokeRepeating("Jump", 0f, jumpInterval);
//    }

//    void UndergroundBehavior()
//    { 
//        // ��Boss��λ�����õ�����
//        float undergroundY = -10f; // ���µ�Y��λ�ã����Ը���ʵ���������
//        transform.position = new Vector3(transform.position.x, undergroundY, transform.position.z);

//        // ���÷�Ҷ����
//        InvokeRepeating("FlyingLeavesAttack", 0f, 2f);
//        // ʵ�ֵ���״̬�߼�
//        // ����Ƿ���Ҫ�л�������״̬
//    }

//    void AttackBehavior()
//    {
//        // ʵ�ֹ���״̬�߼�
//        // ����Ƿ���Ҫ�л�������״̬
//    }

//    void TakeDamageBehavior()
//    {
//        // ʵ������״̬�߼�
//        // ����Ƿ���Ҫ�л�������״̬
//    }

//    void DieBehavior()
//    {
//        // ʵ������״̬�߼�
//        // ����Ƿ���Ҫ�л�������״̬
//    }

//    public void ChangeState(BossState newState)
//    {
//        currentState = newState;
//    }
//    // ��Ծ����
//    void Jump()
//    {
//        float playerX = playerPosition.position.x;
//        float bossX = transform.position.x;
//        float direction = Mathf.Sign(playerX - bossX); // �������������ұߣ�Ϊ1������ߣ�Ϊ-1
//        // ����ˮƽ���Ĵ�С
//        float horizontalForce = jumpForce * 0.5f; // ���Ե�����������Կ���ˮƽ���Ĵ�С
//        // �ϳ���Ծ��
//        Vector2 jumpVector = new Vector2(direction * horizontalForce, jumpForce);
//        // Ӧ����
//        rb.AddForce(jumpVector, ForceMode2D.Impulse);
//        if (rb.velocity.y > 0.1f)
//        {
//            rb.gravityScale = upGravity;
//        }
//        else if (rb.velocity.y < -0.1f)//&&!isfloating)
//        {
//            rb.gravityScale = downGravity;
//        }
//    }
//}
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
    bool CanJump;
    float jumpTimer;
    bool StartLeafAttack = false;

    public bool isGround;

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

    // boss����
    void FollowPlayerXAxis()
    {
        if (playerPosition != null)
        {
            float playerX = playerPosition.position.x;
            float bossX = transform.position.x;
            float distanceToPlayer = Mathf.Abs(playerX - bossX);

            // �����ƶ�����
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
        // ��Boss��λ�����õ�����
        float undergroundY = -10f; // ���µ�Y��λ�ã����Ը���ʵ���������
        transform.position = new Vector3(transform.position.x, undergroundY, transform.position.z);

        // ���÷�Ҷ����
        if (!StartLeafAttack)
        {
        StartCoroutine(FlyingLeavesAttackRoutine());
        StartLeafAttack = true;
        }
        // ʵ�ֵ���״̬�߼�
        // ����Ƿ���Ҫ�л�������״̬
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
            yield return new WaitForSeconds(5f);
        }
    }
}