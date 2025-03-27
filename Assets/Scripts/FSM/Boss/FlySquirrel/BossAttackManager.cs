using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//攻击系统
public class BossAttackManager : MonoBehaviour
{

    public GameObject fallingFruitPrefab;// 水果预制体
    public GameObject explosiveFruitPrefab;//爆炸水果预制体
    public GameObject giantFruitPrefab;//巨大水果预制体
    public GameObject leafProjectilePrefab;//飞叶预制体
    //FallingFruitsAttack
    public float fallingFruitsAttackInterval = 0.5f; // 攻击间隔时间
    public float fallingFruitsAttackDuration = 5f; // 攻击持续时间
    public float timeBetweenFruits = 0.3f; // 每次丢水果的时间间隔
    public float fallingSpeed = 10f; // 水果下落速度
    public Transform fruitSpawnPoint; // 水果生成位置
    private float fallingFruitsAttackTimer; // 丢水果计时器
    private float fallingFruitsAttackDurationTimer; // 攻击持续时间计时器
    //FlyingLeavesAttack
    public float leafSpeed = 5f; // 飞叶的速度
    public float spawnInterval = 3f; // 发射间隔时间
    private Transform playerTransform; // 玩家的Transform组件
    public float playerY; // 玩家的Y轴位置
    public float playerX; // 玩家的X轴位置
    private Vector2 leftSpawnPoint; // 左侧发射点
    private Vector2 rightSpawnPoint; // 右侧发射点
    #region 巨大水果攻击
    public Transform giantFruitSpawnPoint; // 巨大水果生成位置
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
        // 播放攻击动画和音效
        /*
        animator.SetTrigger("FallingFruitsAttack");
        audioSource.PlayOneShot(fallingFruitsAttackSound);
        */
        // 攻击持续时间计时器
        if (fallingFruitsAttackDuration <= 0)
        {
            // 攻击结束，重置计时器
            fallingFruitsAttackDuration = fallingFruitsAttackInterval;
            // 可以在这里添加攻击结束后的逻辑，比如切换回空中状态
            return;
        }

        // 在攻击持续时间内，按照设定的时间间隔丢水果
        if (fallingFruitsAttackTimer <= 0)
        {
            // 生成水果
            fruitSpawnPoint = stateManager.bossPosition;
            GameObject fruit = Instantiate(fallingFruitPrefab, fruitSpawnPoint.position, Quaternion.identity);
            // 设置水果的下落速度
            fruit.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallingSpeed);
            // 设置水果不可穿过平台的逻辑
            fruit.GetComponent<FruitBehavior>().canPassThroughPlatforms = false;

            // 重置丢水果计时器
            fallingFruitsAttackTimer = timeBetweenFruits;
        }
        else
        {
            fallingFruitsAttackTimer -= Time.deltaTime;
        }

        // 减少攻击持续时间计时器
        fallingFruitsAttackDuration -= Time.deltaTime;
        // 实现疯狂丢水果攻击逻辑
        // 定时生成水果并向下抛掷
    }

    public void ExplosiveFruitsAttack()
    {
        // 实现爆炸水果攻击逻辑
    }

    public void GiantFruitsAttack()
    {
        Vector2 giantFruitSpawnPoint = new Vector2(playerX, playerY + 20f); // 修改生成位置的y坐标为playerY + 20f
        GameObject giantFruit = Instantiate(giantFruitPrefab, giantFruitSpawnPoint, Quaternion.identity);
        giantFruit.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5f); // 修改下落速度为5
    }

    public void FlyingLeavesAttack()
    {
        SpawnLeaf();
    }
    void SpawnLeaf()
    {
        // 从左侧发射飞叶
        GameObject leftLeaf = Instantiate(leafProjectilePrefab, leftSpawnPoint, Quaternion.identity);
        Rigidbody2D leftRigidbody = leftLeaf.GetComponent<Rigidbody2D>();
        leftRigidbody.velocity = new Vector2(leafSpeed, 0); // 向右移动
        

        // 从右侧发射飞叶
        GameObject rightLeaf = Instantiate(leafProjectilePrefab, rightSpawnPoint, Quaternion.identity);
        Rigidbody2D rightRigidbody = rightLeaf.GetComponent<Rigidbody2D>();
        rightRigidbody.velocity = new Vector2(-leafSpeed, 0); // 向左移动
    }
}
