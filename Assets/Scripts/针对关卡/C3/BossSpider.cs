using UnityEngine;

public class BossSpider : MonoBehaviour
{
    // 预设资源
    public GameObject webPrefab; // 蜘蛛网预制体
    public GameObject eggPrefab; // 蜘蛛卵预制体
    public GameObject venomPrefab; // 毒液预制体
    public GameObject player; // 玩家对象
    public GameObject lightSource; // 灯光对象

    // 参数
    [Header("灯光")]
    public float darkPhaseDuration = 10f; // 黑暗阶段持续时间
    public float lightPhaseDuration = 5f; // 光亮阶段持续时间
    [Header("蛛网")]
    public float webCooldown = 1f; // 蜘蛛网冷却时间
    public float webSpeed = 5f; // 蜘蛛网速度
    public GameObject webShootPosition; // 蜘蛛网射击位置
    [Header("蛛卵")]
    public float eggCooldown = 6f; // 蜘蛛卵冷却时间
    public float eggRange = 3f; // 蜘蛛卵范围
    [Header("毒液")]
    public float venomCooldown = 2f; // 毒液冷却时间
    public float venomSpeed = 5f; // 毒液速度
    public GameObject venomShootPosition; // 毒液射击位置

    // 状态
    private bool isMovingRight = true; // 是否向右移动
    public float patrolSpeed = 1f; // 巡逻速度
    public Transform patrolLeft; // 巡逻左边界
    public Transform patrolRight; // 巡逻右边界
    private bool isDarkPhase = true; // 是否是黑暗阶段
    private float darkPhaseTimer = 0f;
    private float lightPhaseTimer = 0f;
    private float webTimer = 0f;
    private float eggTimer = 0f;
    private float venomTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 找到玩家对象
        // 初始状态
        lightSource.SetActive(false); // 关闭灯光
    }

    void Update()
    {
        // 切换阶段
        if (isDarkPhase)
        {
            darkPhaseTimer += Time.deltaTime;
            if (darkPhaseTimer >= darkPhaseDuration)
            {
                isDarkPhase = false;
                darkPhaseTimer = 0f;
                lightSource.SetActive(true); // 打开灯光
            }
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, Mathf.Lerp(gameObject.transform.position.y, -4, Time.deltaTime * 2f));
            HandleDarkPhase();
        }
        else
        {
            lightPhaseTimer += Time.deltaTime;
            if (lightPhaseTimer >= lightPhaseDuration)
            {
                isDarkPhase = true;
                lightPhaseTimer = 0f;
                lightSource.SetActive(false); // 关闭灯光
            }
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, Mathf.Lerp(gameObject.transform.position.y, -13, Time.deltaTime * 2f));
            Patrol();
            HandleLightPhase();
        }
    }

    void HandleDarkPhase()
    {
        // 吐蜘蛛网
        webTimer += Time.deltaTime;
        if (webTimer >= webCooldown)
        {
            webTimer = 0f;
            ShootWeb();
        }
    }

    void HandleLightPhase()
    {
        eggTimer += Time.deltaTime;
        if (eggTimer >= eggCooldown)
        {
            eggTimer = 0f;
            SpawnEggs();
        }

        venomTimer += Time.deltaTime;
        if (venomTimer >= venomCooldown)
        {
            venomTimer = 0f;
            ShootVenom();
        }
    }

    void ShootWeb()
    {
        // 朝玩家发射蜘蛛网
        Vector2 direction = (player.transform.position - webShootPosition.transform.position).normalized;
        GameObject web = Instantiate(webPrefab, webShootPosition.transform.position, Quaternion.identity);
        Rigidbody2D webRigidbody = web.GetComponent<Rigidbody2D>();
        webRigidbody.gravityScale = 0f;
        webRigidbody.AddForce(direction * webSpeed, ForceMode2D.Impulse);
    }

    void ShootVenom()
    {
        // 朝正上方发射毒液
        Vector2 direction = Vector2.up; // 正上方方向
        GameObject venom = Instantiate(venomPrefab, venomShootPosition.transform.position, Quaternion.identity);
        Rigidbody2D venomRigidbody = venom.GetComponent<Rigidbody2D>();
        venomRigidbody.gravityScale = 1f; // 确保毒液受到重力影响
        venomRigidbody.AddForce(direction * venomSpeed, ForceMode2D.Impulse);
    }

    void SpawnEggs()
    {
        // 在Boss周围生成蜘蛛卵
        for (int i = 0; i < 2; i++)
        {
            Vector2 eggPosition = (Vector2)this.transform.position + new Vector2(Random.Range(-eggRange, eggRange), Random.Range(-eggRange, eggRange));
            Instantiate(eggPrefab, eggPosition, Quaternion.identity);
        }
    }

    void Patrol()
    {
        // 正常行走巡逻
        if (isMovingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            // 向右移动
            transform.position = Vector2.MoveTowards(transform.position, patrolRight.position, patrolSpeed * Time.deltaTime);
            if (transform.position.x >= patrolRight.position.x)
            {
                isMovingRight = false; // 到达右边界，切换方向
            }
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            // 向左移动
            transform.position = Vector2.MoveTowards(transform.position, patrolLeft.position, patrolSpeed * Time.deltaTime);
            if (transform.position.x <= patrolLeft.position.x)
            {
                isMovingRight = true; // 到达左边界，切换方向
            }
        }
    }
}