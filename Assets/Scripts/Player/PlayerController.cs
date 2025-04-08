
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Animator anim;
    
    //地面检测
    public LayerMask groundLayer; // 地面层，用于检测是否在地面上
    public Transform groundCheck; // 地面检测点
    public Vector2 groundCheckSize = new Vector2(1f, 0.1f);//地面检测范围
    [Header("声波")]
    public GameObject lowBulletPrefab;//低频声波预设体
    public GameObject highBulletPrefab;//高频声波预设体
    
    public float shootForce = 3.0f;//射击力度
    public float keepTime = 1.0f;//声波持续时间

    [Header("属性")]
    public int health = 3; //生命值
    public float moveSpeed = 8f; // 移动速度
    public float jumpForce = 16f; // 跳跃力度
    

    public float upGravity;//跳跃时重力大小
    public float downGravity;//下落时重力大小

    [Header("状态")]
    public bool isGround;
    public bool isAttack;
    public bool isDead;
    public bool canMove = true;
    public bool canAttack;

    [Header("主角行动状态")]
    private string state_type = "isMoveAble";//isMoveAble,cantMoveAble

    #region 以下为私有属性
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public VoiceController voiceController;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //voiceController = GameObject.Find("VoiceController").GetComponent<VoiceController>();
        voiceController = null;

        
    }

    void Update()
    {
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
    }

    public void Move()
    {
        // 检测玩家是否在地面上
        isGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        // 获取输入
        float moveInput = Input.GetAxis("Horizontal");
        // 移动玩家
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput < 0) //根据输入翻转玩家
        {
           spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    public void Jump()
    {
        if (isGround && Input.GetButtonDown("Jump"))
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
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            isAttack = true;
            // //获取鼠标坐标
            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
            Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            //实例化声波
            GameObject bullet = Instantiate(lowBulletPrefab, transform.position, Quaternion.identity);
            BattleWave battleWave = bullet.GetComponent<BattleWave>();
            battleWave.Initialize(keepTime);
            //声波方向
            Vector2 direction = (worldPosition - position2D).normalized;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            //发射声波
            rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
            //增加过载
            if (voiceController!= null){voiceController.AddVoice();}
        }else if (Input.GetMouseButtonDown(1) && canAttack){
            isAttack = true;
            //获取鼠标坐标
            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
            Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            //实例化声波
            GameObject bullet = Instantiate(highBulletPrefab, transform.position, Quaternion.identity);
            BattleWave battleWave = bullet.GetComponent<BattleWave>();
            battleWave.Initialize(keepTime);
            //声波方向
            Vector2 direction = (worldPosition - position2D).normalized;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            //发射声波
            rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
            //增加过载
            if (voiceController!= null){voiceController.AddVoice();}
        }else {
            isAttack = false;
        }
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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enmey"){
            TakeDamage(1);
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
}