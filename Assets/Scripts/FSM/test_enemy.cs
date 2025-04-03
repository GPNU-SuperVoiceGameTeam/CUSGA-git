using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public Transform player; // 主角Transform
    public float patrolSpeed = 2f; // 巡逻速度
    public float attackSpeed = 4f; // 攻击速度
    public float detectionRange = 5f; // 检测范围
    public float stopDistance = 1f; // 停止距离
    public GameObject exclamationMarkPrefab; // 头上的叹号预制体

    private Vector3[] patrolPoints = new Vector3[2]; // 巡逻点数组
    private int currentPatrolPointIndex = 0; // 当前巡逻点索引
    private bool isAttacking = false; // 是否处于攻击状态
    private GameObject exclamationMarkInstance; // 头上叹号实例
    private Rigidbody rb; // 刚体组件

    void Start()
    {
        if (player == null) // 检查主角是否赋值
        {
            Debug.LogError("Player not assigned in the Inspector."); // 如果未赋值，输出错误信息
            return;
        }

        // 初始化巡逻点
        patrolPoints[0] = transform.position + new Vector3(-5, 0, 0); // 第一个巡逻点位置
        patrolPoints[1] = transform.position + new Vector3(5, 0, 0); // 第二个巡逻点位置

        rb = GetComponent<Rigidbody>(); // 获取刚体组件

        if (rb == null) // 检查刚体组件是否存在
        {
            Debug.LogError("Rigidbody component missing from enemy object."); // 如果不存在，输出错误信息
        }
        else
        {
            rb.isKinematic = false; // 确保刚体不是动力学刚体
            rb.useGravity = false; // 取消重力影响
        }
    }

    void Update()
    {
        if (rb == null) return; // 如果刚体组件不存在，直接返回

        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // 计算敌人与主角之间的距离

        if (distanceToPlayer <= detectionRange) // 如果距离在检测范围内
        {
            EnterAttackState(); // 进入攻击状态
        }
        else // 否则
        {
            EnterPatrolState(); // 进入巡逻状态
        }
    }

    void EnterPatrolState()
    {
        if (!isAttacking && rb != null) // 如果不在攻击状态且刚体组件存在
        {
            MoveToNextPatrolPoint(); // 移动到下一个巡逻点
        }
    }

    void EnterAttackState()
    {
        if (!isAttacking && rb != null) // 如果不在攻击状态且刚体组件存在
        {
            isAttacking = true; // 设置为攻击状态
            CancelInvoke(); // 取消所有重复调用

            if (exclamationMarkInstance == null) // 如果叹号实例不存在
            {
                exclamationMarkInstance = Instantiate(exclamationMarkPrefab, player.position + new Vector3(0, 1.5f, 0), Quaternion.identity); // 实例化叹号预制体
            }

            InvokeRepeating("AttackPlayer", 0f, 2f); // 每2秒执行一次攻击行为
        }
    }

    void AttackPlayer()
    {
        if (rb == null) return; // 如果刚体组件不存在，直接返回

        transform.LookAt(player); // 面向主角
        Vector3 directionToPlayer = (player.position - transform.position).normalized; // 计算面向主角的方向

        // 加速冲向玩家
        rb.MovePosition(transform.position + directionToPlayer * attackSpeed * Time.deltaTime);

        // 到达停止距离后减速并面向玩家
        if (Vector3.Distance(transform.position, player.position) <= stopDistance) // 如果到达停止距离
        {
            CancelInvoke(); // 停止重复调用
            Invoke("StopAndFacePlayer", 0f); // 立即执行停止行为
            Invoke("ResumeAttack", 1f); // 1秒后恢复攻击行为
        }
    }

    void StopAndFacePlayer()
    {
        if (rb == null) return; // 如果刚体组件不存在，直接返回

        transform.LookAt(player); // 面向主角
    }

    void ResumeAttack()
    {
        InvokeRepeating("AttackPlayer", 0f, 2f); // 恢复重复攻击行为
    }

    void MoveToNextPatrolPoint()
    {
        if (rb == null) return; // 如果刚体组件不存在，直接返回

        Vector3 targetPosition = patrolPoints[currentPatrolPointIndex]; // 获取目标巡逻点位置
        transform.LookAt(targetPosition); // 面向目标巡逻点
        rb.MovePosition(transform.position + Vector3.forward * patrolSpeed * Time.deltaTime); // 移动到目标巡逻点

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f) // 如果接近目标巡逻点
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length; // 切换到下一个巡逻点
        }

        Debug.Log($"Moving to patrol point {currentPatrolPointIndex}: {targetPosition}"); // 输出调试信息
    }

    void OnDestroy()
    {
        if (exclamationMarkInstance != null) // 如果叹号实例存在
        {
            Destroy(exclamationMarkInstance); // 销毁叹号实例
        }
    }
}



