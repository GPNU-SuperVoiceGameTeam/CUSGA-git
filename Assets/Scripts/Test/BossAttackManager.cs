using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//攻击系统
public class BossAttackManager : MonoBehaviour
{
    public GameObject fallingFruitPrefab;// 水果预制体
    public GameObject explosiveFruitPrefab;
    public GameObject giantFruitPrefab;
    public GameObject leafProjectilePrefab;
    //FallingFruitsAttack
    public float fallingFruitsAttackInterval = 1f; // 攻击间隔时间
    public float fallingFruitsAttackDuration = 5f; // 攻击持续时间
    public float timeBetweenFruits = 0.5f; // 每次丢水果的时间间隔
    public float fallingSpeed = 10f; // 水果下落速度
    public Transform fruitSpawnPoint; // 水果生成位置
    private float fallingFruitsAttackTimer; // 丢水果计时器
    private float fallingFruitsAttackDurationTimer; // 攻击持续时间计时器

    private BossStateManager stateManager;

    void Start()
    {
        stateManager = GetComponent<BossStateManager>();
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
        // 实现巨大水果攻击逻辑
    }

    public void FlyingLeavesAttack()
    {
        // 实现飞叶攻击逻辑
        // 从屏幕两侧发射叶子
    }
}
