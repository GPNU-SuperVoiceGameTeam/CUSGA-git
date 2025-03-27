using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ϵͳ
public class BossAttackManager : MonoBehaviour
{

    public GameObject fallingFruitPrefab;// ˮ��Ԥ����
    public GameObject explosiveFruitPrefab;//��ըˮ��Ԥ����
    public GameObject giantFruitPrefab;//�޴�ˮ��Ԥ����
    public GameObject leafProjectilePrefab;//��ҶԤ����
    //FallingFruitsAttack
    public float fallingFruitsAttackInterval = 0.5f; // �������ʱ��
    public float fallingFruitsAttackDuration = 5f; // ��������ʱ��
    public float timeBetweenFruits = 0.3f; // ÿ�ζ�ˮ����ʱ����
    public float fallingSpeed = 10f; // ˮ�������ٶ�
    public Transform fruitSpawnPoint; // ˮ������λ��
    private float fallingFruitsAttackTimer; // ��ˮ����ʱ��
    private float fallingFruitsAttackDurationTimer; // ��������ʱ���ʱ��
    //FlyingLeavesAttack
    public float leafSpeed = 5f; // ��Ҷ���ٶ�
    public float spawnInterval = 3f; // ������ʱ��
    private Transform playerTransform; // ��ҵ�Transform���
    public float playerY; // ��ҵ�Y��λ��
    public float playerX; // ��ҵ�X��λ��
    private Vector2 leftSpawnPoint; // ��෢���
    private Vector2 rightSpawnPoint; // �Ҳ෢���
    #region �޴�ˮ������
    public Transform giantFruitSpawnPoint; // �޴�ˮ������λ��
    #endregion
    private BossStateManager stateManager;

    void Start()
    {
        stateManager = GetComponent<BossStateManager>();
    }
    public void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerY = playerTransform.position.y;
        playerX = playerTransform.position.x;
        leftSpawnPoint = new Vector2(-Camera.main.orthographicSize * 2, playerY-1);
        rightSpawnPoint = new Vector2(Camera.main.orthographicSize * 2, playerY+1);
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
        Vector2 giantFruitSpawnPoint = new Vector2(playerX, playerY + 20f); // �޸�����λ�õ�y����ΪplayerY + 20f
        GameObject giantFruit = Instantiate(giantFruitPrefab, giantFruitSpawnPoint, Quaternion.identity);
        giantFruit.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5f); // �޸������ٶ�Ϊ5
    }

    public void FlyingLeavesAttack()
    {
        SpawnLeaf();
    }
    void SpawnLeaf()
    {
        // ����෢���Ҷ
        GameObject leftLeaf = Instantiate(leafProjectilePrefab, leftSpawnPoint, Quaternion.identity);
        Rigidbody2D leftRigidbody = leftLeaf.GetComponent<Rigidbody2D>();
        leftRigidbody.velocity = new Vector2(leafSpeed, 0); // �����ƶ�
        

        // ���Ҳ෢���Ҷ
        GameObject rightLeaf = Instantiate(leafProjectilePrefab, rightSpawnPoint, Quaternion.identity);
        Rigidbody2D rightRigidbody = rightLeaf.GetComponent<Rigidbody2D>();
        rightRigidbody.velocity = new Vector2(-leafSpeed, 0); // �����ƶ�
    }
}
