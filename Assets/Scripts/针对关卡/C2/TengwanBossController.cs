using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public enum State
    {
        Teleport,
        Attack
    }

    public State currentState;

    public Transform[] platforms; // 所有平台的位置
    public float attackInterval = 2f; // 攻击间隔时间
    public GameObject vinePrefab; // 藤蔓预制体
    public Transform player; // 玩家的位置

    private float attackTimer;
    public Vector2 targetPosition;
    public bool isHit;
    public bool ischanging;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Attack;
        transform.position = platforms[0].position;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Teleport:
                TeleportState();
                break;
            case State.Attack:
                AttackState();
                break;
        }
    }

    void TeleportState()
    {
        int randomIndex = Random.Range(0, platforms.Length);
        if(!ischanging){
            targetPosition = platforms[randomIndex].position;
            ischanging = true;
        }
        

        // 移动到目标平台
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 20.0f * Time.deltaTime);

        // 如果到达目标位置，切换到攻击状态
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            ischanging = false;
            isHit = false;
            currentState = State.Attack;
            attackTimer = 0f; // 重置攻击计时器
        }
    }

    void AttackState()
{
    attackTimer += Time.deltaTime;

    // 每隔一段时间攻击一次
    if (attackTimer >= attackInterval)
    {
        // 在玩家脚下生成藤蔓（初始为缩回状态）
        GameObject vine = Instantiate(vinePrefab, player.position, Quaternion.identity);

        BossVineAttack vineScript = vine.GetComponent<BossVineAttack>();
        if (vineScript != null)
        {
            vineScript.Initialize();
        }

        attackTimer = 0f; // 重置攻击计时器
    }

    // 如果被攻击，切换到传送状态
    if (isHit)
    {
        currentState = State.Teleport;
        attackTimer = 0f; // 重置攻击计时器
    }
}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("lowWave"))
        {
            isHit = true;
            Destroy(collision.gameObject);
        }
    }
}