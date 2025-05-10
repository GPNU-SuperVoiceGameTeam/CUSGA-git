using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{   public enum specialBulletType{
        jumpWave,
        lightWave,
        guardWave
    }
    public RebornPoint rebornPoint;
    public Animator anim;
    public BattleWaveVoicer battleWaveVoicer;
    //地面检测
    public LayerMask groundLayer; // 地面层，用于检测是否在地面上
    public Transform groundCheck; // 地面检测点
    public Vector2 groundCheckSize = new Vector2(1f, 0.1f);//地面检测范围
    [Header("重生")]
    // public Vector2 spawnPoint; // 重生点
    // public GameObject playerPrefab; // 重生预设体
    [Header("声波")]
    public GameObject lowBulletPrefab;//低频声波预设体
    public GameObject highBulletPrefab;//高频声波预设体
    public GameObject[] specialBulletPrefab;//特殊声波预设体
    public specialBulletType specialBullet;
    
    public float shootForce = 3.0f;//射击力度
    public float keepTime = 1.0f;//声波持续时间
    public float attackCooldown = 0.5f; // 攻击冷却时间
    private float nextAttackTime = 0f; // 下一次可以攻击的时间

    [Header("属性")]
    public int maxHealth; //最大生命值
    public int health; //生命值
    public float moveSpeed = 8f; // 移动速度
    public float jumpForce = 16f; // 跳跃力度
    public AnimationCurve movementCurve;//运动曲线
    private float CurrentSpeed;
    private float AddTime;
    private float lastTimeOnGround = 0f;
    public float coyoteTimeDuration = 0.15f; // 可调：缓冲时间长短
    public float fallMultiplier = 2.5f; // 下落更快更自然
    public float lowJumpMultiplier = 2f; // 松开跳跃键提前下落

    public float upGravity;//跳跃时重力大小
    public float downGravity;//下落时重力大小

    [Header("状态")]
    public bool isGround;
    public bool isAttack;
    public bool isDead;
    public bool isInvincible;
    public bool canMove = true;
    [Header("背包")]
    public bool canOpenBackpack;
    public GameObject backpack;
    public bool isOpen;
    [Header("解锁")]
    public bool canAttack;
    public bool lowWaveUnlock;
    public bool highWaveUnlock;
    public bool lightWaveUnlock;
    public bool jumpWaveUnlock;
    public bool guardWaveUnlock;

    [Header("主角行动状态")]
    private string state_type = "isMoveAble";//isMoveAble,cantMoveAble
    public bool isbeWebControl = false;

    #region 以下为私有属性
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public VoiceController voiceController;
    private float moveInput;
    float originalMoveSpeed;
    float originalUpGravity;
    float originalDownGravity;
    float originalJumpForce;
    public MusicChange musicChange;
    public GameObject menu;
    public GameObject rebornText;
    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        maxHealth = 5;
        health = maxHealth;
        originalMoveSpeed = moveSpeed;
        originalUpGravity = upGravity;
        originalDownGravity = downGravity;
        originalJumpForce = jumpForce;
        musicChange = GameObject.FindGameObjectWithTag("MusicChange").GetComponent<MusicChange>();
        // spawnPoint = transform.position;
    }
    void Update()
    {
        if(isDead){
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }else{
        }
        OpenMenu();
        openBackpack();
        if(canMove){
            switch (state_type)
            {
                case "isMoveAble":
                    Move();
                    Jump();
                    Attack();
                    break;
                case "cantMoveAble":
                    break;
            }
        }else{
            Reborn();
        }
        ChangePlayerSpeed(isbeWebControl);
    }

    public void Move()
    {
        // 检测玩家是否在地面上
        isGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        // 获取输入
        if(canMove){
            moveInput = Input.GetAxis("Horizontal");
            // 移动玩家

            //曲线型移动
            CurrentSpeed = Judement_CurveAddUpSpeed();
            rb.velocity = new Vector2(moveInput * CurrentSpeed, rb.velocity.y);

            if (moveInput < 0) //根据输入翻转玩家
            {
            spriteRenderer.flipX = true;
            }
            else if (moveInput > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    public void Jump()
    {
        // 检测是否刚刚离地（可用于跳跃）
        if (isGround)
        {
            lastTimeOnGround = Time.time; // 更新最后一次着陆时间
        }

        // 判断是否可以跳跃：原本条件 || 离开地面但仍在coyote time内
        bool canJump = isGround || (Time.time - lastTimeOnGround) <= coyoteTimeDuration;

        if (canJump && Input.GetButton("Jump"))
        {
            battleWaveVoicer.music[5].GetComponent<AudioSource>().Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.Play("PlayerJump", 0, 0f);

            // 可选：重置最后着陆时间，防止多次跳跃
            lastTimeOnGround = -coyoteTimeDuration; // 或者直接设为 -1
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f); // 清除垂直速度
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    
    
    public void Attack()
    {
        // 获取鼠标坐标
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);

        if (Time.time >= nextAttackTime) // 检查是否已经过了冷却时间
        {
            if (Input.GetMouseButtonDown(0) && canAttack && lowWaveUnlock)
            {
                isAttack = true;
                // 实例化声波
                GameObject bullet = Instantiate(lowBulletPrefab, transform.position, Quaternion.identity, transform);
                BattleWave battleWave = bullet.GetComponent<BattleWave>();
                battleWave.Initialize(keepTime);
                // 声波方向
                Vector2 direction = (worldPosition - position2D).normalized;
                bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                Rigidbody2D gunRb = bullet.GetComponent<Rigidbody2D>();
                // 发射声波
                gunRb.AddForce(direction * shootForce, ForceMode2D.Impulse);
                int lowWaveVoice = Random.Range(0, 3);
                battleWaveVoicer.music[lowWaveVoice].GetComponent<AudioSource>().Play();
                // 增加过载
                voiceController.AddVoice(10);
                nextAttackTime = Time.time + attackCooldown; // 更新下一次攻击时间
            }
            else if (Input.GetMouseButtonDown(1) && canAttack && highWaveUnlock)
            {
                isAttack = true;
                // 实例化声波
                GameObject bullet = Instantiate(highBulletPrefab, transform.position, Quaternion.identity, transform);
                BattleWave battleWave = bullet.GetComponent<BattleWave>();
                battleWave.Initialize(keepTime);
                // 声波方向
                Vector2 direction = (worldPosition - position2D).normalized;
                bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                Rigidbody2D gunRb = bullet.GetComponent<Rigidbody2D>();
                // 发射声波
                gunRb.AddForce(direction * shootForce, ForceMode2D.Impulse);
                battleWaveVoicer.music[3].GetComponent<AudioSource>().Play();
                // 增加过载
                voiceController.AddVoice(10);
                nextAttackTime = Time.time + attackCooldown; // 更新下一次攻击时间
            }
            else if (Input.GetMouseButtonDown(2)||Input.GetKeyDown(KeyCode.LeftShift) && canAttack)
            {
                isAttack = true;
                switch (specialBullet)
                {
                    case specialBulletType.jumpWave:
                        if (jumpWaveUnlock){
                            jumpWave();
                        }
                        break;
                    case specialBulletType.lightWave:
                        if (lightWaveUnlock){
                            LightWave();
                        }
                        break;
                    case specialBulletType.guardWave:
                        if(guardWaveUnlock && voiceController.voice >= 60){
                            GuardWave();
                        }
                        break;
                }

                nextAttackTime = Time.time + attackCooldown; // 更新下一次攻击时间
            }
            else
            {
                isAttack = false;
            }
        }
    }

    public void openBackpack()
    {
        if(canOpenBackpack){
            if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 切换背包的显示状态
            backpack.SetActive(!backpack.activeSelf);

            // 根据背包的显示状态设置 canAttack 和 canMove
            if (backpack.activeSelf)
            {
                Time.timeScale = 0;
                canAttack = false;
                canMove = false;
            }
            else
            {
                Time.timeScale = 1;
                canAttack = true;
                canMove = true;
            }
        }
        }
        
    }

    public void specialBulletSelect(specialBulletType type){
        specialBullet = type;
    }
    

    public void TakeDamage(int damage)
    {   
        battleWaveVoicer.battle[1].GetComponent<AudioSource>().Play();
        if(isInvincible){
            return;
        }else{
            health -= damage;
        if (health <= 0)
        {
            Die();
        }
        }
        
    }

    public void Die()
    {
        isDead = true;
        canMove = false;
        canAttack = false;
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<BoxCollider2D>().enabled = false;
        anim.Play("PlayerDead", 0, 0f);
        rebornText.SetActive(true);
    }
    public void Reborn(){
        if(isDead){
            if(Input.GetKeyDown(KeyCode.R)){
                rebornText.SetActive(false);
                // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                musicChange.SwitchBackToOriginalMusic();
                rebornPoint.OnPlayerDeath();
                health = maxHealth;
                transform.position = rebornPoint.spawnPoint.position;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rebornPoint.GenerateReactiveItems();
                isDead = false;
                canMove = true;
                canAttack = true;
                rb.isKinematic = false;
                this.GetComponent<BoxCollider2D>().enabled = true;
                anim.Play("PlayerIdle", 0, 0f);
            }
        }
    }
    float Judement_CurveAddUpSpeed()//用曲线设定玩家移动速度
    {
        if(moveInput != 0)
        {
            CurrentSpeed = moveSpeed*movementCurve.Evaluate(AddTime);
            AddTime +=Time.deltaTime;
            return CurrentSpeed;

        }
        else
        {
            AddTime = 0;
            return 0;

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(groundCheck.position, groundCheckSize);
        }
    }
    #region 特殊声波方法
    #region 跳跃声波
    private void jumpWave(){
        voiceController.AddVoice(20);
        float jumpWaveKeepTime = 1.0f;
        float jumpWaveShootForce = 6.0f;
        // 获取鼠标坐标
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        GameObject bullet = Instantiate(specialBulletPrefab[0], transform.position, Quaternion.identity);
        BattleWave battleWave = bullet.GetComponent<BattleWave>();
        battleWave.Initialize(jumpWaveKeepTime);
        // 声波方向
        Vector2 direction = (worldPosition - position2D).normalized;
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        Rigidbody2D gunRb = bullet.GetComponent<Rigidbody2D>();
        // 发射声波
        gunRb.AddForce(direction * jumpWaveShootForce, ForceMode2D.Impulse);
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(-direction * 15, ForceMode2D.Impulse);
        battleWaveVoicer.music[6].GetComponent<AudioSource>().Play();
    }
    #endregion
    #region 护盾声波
    private void GuardWave()
    {
    voiceController.AddVoice(-60);

    float guardWaveKeepTime = 1.0f;
    GameObject bullet = Instantiate(specialBulletPrefab[2], transform.position, Quaternion.identity, transform);
    BattleWave battleWave = bullet.GetComponent<BattleWave>();
    battleWave.Initialize(guardWaveKeepTime);

    // 启动协程处理无敌状态的开启与关闭
    StartCoroutine(ApplyInvincibilityForDuration(guardWaveKeepTime));
    }

    // 协程：设置无敌状态并持续一段时间
    private IEnumerator ApplyInvincibilityForDuration(float duration)
    {
        isInvincible = true; // 开始无敌

        yield return new WaitForSeconds(duration); // 等待指定时间

        isInvincible = false; // 取消无敌
    }
    #endregion
    #region 照明声波
    private void LightWave(){
        voiceController.AddVoice(10);
        float lightWaveKeepTime = 6.0f;
        GameObject bullet = Instantiate(specialBulletPrefab[1], transform.position, Quaternion.identity, transform);
        Light2D light2D = bullet.GetComponent<Light2D>();
        BattleWave battleWave = bullet.GetComponent<BattleWave>();
        battleWave.Initialize(lightWaveKeepTime);
    }
    #endregion

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("SpiderWeb"))
        {
            isbeWebControl = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SpiderWeb"))
        {
            isbeWebControl = false;
        }
    }

    void ChangePlayerSpeed(bool isInSlowZone)
    {
        if (isInSlowZone)
        {
            moveSpeed = 1;
            upGravity = 5;
            downGravity = 0.01f;
            jumpForce = 5;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
            upGravity = originalUpGravity;
            downGravity = originalDownGravity;
            jumpForce = originalJumpForce;
        }
    }

    void OpenMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isOpen){
                isOpen = true;
                Time.timeScale = 0;
                menu.SetActive(true);
            }else{
                isOpen = false;
                Time.timeScale = 1;
                menu.SetActive(false);
            }
            
        }
    }


    
    #endregion
}