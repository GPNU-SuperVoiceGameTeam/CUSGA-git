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
    public BossState currentState;//��ǰ״̬
    private BossStates bossStates;//��ȡ����ֵϵͳ
    private Animator animator;
    private BossAttackManager attackManager;//��ȡ����ϵͳ
    public Transform bossPosition;//bossλ��
    public Transform playerPosition;//��ȡ���λ��
    public float moveSpeed = 5f;//�ƶ��ٶ�
    public float slowDownDistance = 2f; // ��ʼ���ٵľ���
    public float stopDistance = 0.5f; // ֹͣ�ƶ��ľ���

    void Start()
    {
        currentState = BossState.Airborne;//��ʼ��״̬
        //bossStates = GetComponent<BossStates>();
        //animator = GetComponent<Animator>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform; // ��ȡ��Ҷ����Transform���
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
    //boss����
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
                transform.position = new Vector3(
                    transform.position.x + direction * moveSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );
            }
            // ���������ҽϽ��������ƶ�
            else if (distanceToPlayer > stopDistance)
            {
                float slowSpeed = moveSpeed * (distanceToPlayer / slowDownDistance);
                transform.position = new Vector3(
                    transform.position.x + direction * slowSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );
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
        // ʵ�ֿ���״̬�߼�
        // ����Ƿ���Ҫ�л�������״̬
    }

    void GroundBehavior()
    {
        // ʵ�ֵ���״̬�߼�
        // ����Ƿ���Ҫ�л�������״̬
    }

    void UndergroundBehavior()
    {
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

    //״̬ת���߼�
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
    //    // ����״̬ת������...
    //}
}
