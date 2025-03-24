using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ϵͳ
public class BossAttackManager : MonoBehaviour
{
    public GameObject fallingFruitPrefab;// ˮ��Ԥ����
    public GameObject explosiveFruitPrefab;
    public GameObject giantFruitPrefab;
    public GameObject leafProjectilePrefab;
    //FallingFruitsAttack
    public float fallingFruitsAttackInterval = 1f; // �������ʱ��
    public float fallingFruitsAttackDuration = 5f; // ��������ʱ��
    public float timeBetweenFruits = 0.5f; // ÿ�ζ�ˮ����ʱ����
    public float fallingSpeed = 10f; // ˮ�������ٶ�
    public Transform fruitSpawnPoint; // ˮ������λ��
    private float fallingFruitsAttackTimer; // ��ˮ����ʱ��
    private float fallingFruitsAttackDurationTimer; // ��������ʱ���ʱ��

    private BossStateManager stateManager;

    void Start()
    {
        stateManager = GetComponent<BossStateManager>();
    }

    public void FallingFruitsAttack()
    {
        // ���Ź�����������Ч
        /*
        animator.SetTrigger("FallingFruitsAttack");
        audioSource.PlayOneShot(fallingFruitsAttackSound);
        */
        // ��������ʱ���ʱ��
        if (fallingFruitsAttackDuration <= 0)
        {
            // �������������ü�ʱ��
            fallingFruitsAttackDuration = fallingFruitsAttackInterval;
            // ������������ӹ�����������߼��������л��ؿ���״̬
            return;
        }

        // �ڹ�������ʱ���ڣ������趨��ʱ������ˮ��
        if (fallingFruitsAttackTimer <= 0)
        {
            // ����ˮ��
            fruitSpawnPoint = stateManager.bossPosition;
            GameObject fruit = Instantiate(fallingFruitPrefab, fruitSpawnPoint.position, Quaternion.identity);
            // ����ˮ���������ٶ�
            fruit.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallingSpeed);
            // ����ˮ�����ɴ���ƽ̨���߼�
            fruit.GetComponent<FruitBehavior>().canPassThroughPlatforms = false;

            // ���ö�ˮ����ʱ��
            fallingFruitsAttackTimer = timeBetweenFruits;
        }
        else
        {
            fallingFruitsAttackTimer -= Time.deltaTime;
        }

        // ���ٹ�������ʱ���ʱ��
        fallingFruitsAttackDuration -= Time.deltaTime;
        // ʵ�ַ��ˮ�������߼�
        // ��ʱ����ˮ������������
    }

    public void ExplosiveFruitsAttack()
    {
        // ʵ�ֱ�ըˮ�������߼�
    }

    public void GiantFruitsAttack()
    {
        // ʵ�־޴�ˮ�������߼�
    }

    public void FlyingLeavesAttack()
    {
        // ʵ�ַ�Ҷ�����߼�
        // ����Ļ���෢��Ҷ��
    }
}
