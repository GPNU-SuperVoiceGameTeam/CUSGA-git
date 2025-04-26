using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class PlayerController : MonoBehaviour
{   public enum specialBulletType{
        jumpWave,
        lightWave,
        guardWave
    }
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
    

    public float upGravity;//跳跃时重力大小
    public float downGravity;//下落时重力大小

    [Header("状态")]
    public bool isGround;
    public bool isAttack;
    public bool isDead;
    public bool canMove = true;
    [Header("背包")]
    public bool canOpenBackpack;
    public GameObject backpack;
    [Header("解锁")]
    public bool canAttack;
    public bool lowWaveUnlock;
    public bool highWaveUnlock;
    public bool lightWaveUnlock;
    public bool jumpWaveUnlock;

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
    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        // spawnPoint = transform.position;
        maxHealth = 5;
        health = maxHealth;
        originalMoveSpeed = moveSpeed;
        originalUpGravity = upGravity;
        originalDownGravity = downGravity;
        originalJumpForce = jumpForce;
    }
    void Update()
    {
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

            //rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

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
        if (isGround && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.Play("PlayerJump", 0, 0f);
        }


        //根据下降改变重力
        
        if(rb.velocity.y>0.1f)
        {
            rb.gravityScale = upGravity;
        }
        else if(rb.velocity.y < -0.1f)//&&!isfloating)
        {
            rb.gravityScale = downGravity;
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
                GameObject bullet = Instantiate(lowBulletPrefab, transform.position, Quaternion.identity);
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
                GameObject bullet = Instantiate(highBulletPrefab, transform.position, Quaternion.identity);
                BattleWave battleWave = bullet.GetComponent<BattleWave>();
                battleWave.Initialize(keepTime);
                // 声波方向
                Vector2 direction = (worldPosition - position2D).normalized;
                bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                Rigidbody2D gunRb = bullet.GetComponent<Rigidbody2D>();
                // 发射声波
                gunRb.AddForce(direction * shootForce, ForceMode2D.Impulse);
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
                        if (lightWaveUnlock)
                        {
                            LightWave();
                        }
                        break;
                    case specialBulletType.guardWave:
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 切换背包的显示状态
            backpack.SetActive(!backpack.activeSelf);

            // 根据背包的显示状态设置 canAttack 和 canMove
            if (backpack.activeSelf)
            {
                canAttack = false;
                canMove = false;
            }
            else
            {
                canAttack = true;
                canMove = true;
            }
        }
    }

    public void specialBulletSelect(specialBulletType type){
        specialBullet = type;
    }
    

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
        UnityEngine.Debug.Log("玩家死亡");
        // Respawn();
    }

    // public void Respawn(){
    //     transform.position = spawnPoint;
    //     health = maxHealth;
    //     Instantiate(playerPrefab, transform.position, Quaternion.identity);
    //     isDead = false;

    // }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if(collision.gameObject.tag == "Enmey"){
    //         TakeDamage(1);
    //     }

    // }
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
        voiceController.AddVoice(10);
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
        rb.AddForce(-direction * 10, ForceMode2D.Impulse);
    }
    #endregion
    #region 照明声波
    //照明声波
    private void LightWave(){
        voiceController.AddVoice(10);
        float lightWaveKeepTime = 3.0f;
        GameObject bullet = Instantiate(specialBulletPrefab[1], transform.position, Quaternion.identity);
        Light2D light2D = bullet.GetComponent<Light2D>();
        BattleWave battleWave = bullet.GetComponent<BattleWave>();
        battleWave.Initialize(lightWaveKeepTime);
    }
    #endregion
    //护卫声波
    private void GuardWave(){

    }

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


    
    #endregion
}