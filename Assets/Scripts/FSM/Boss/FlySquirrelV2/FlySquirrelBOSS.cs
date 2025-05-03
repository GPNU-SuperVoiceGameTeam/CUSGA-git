using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySquirrelBOSS : Enemy
{
    public enum BossState
    {
    Airborne, // 空中
    Ground, // 地面
    Underground, // 地下
    Attacking, // 攻击
    TakingDamage, // 受击
    Dying // 死亡
    }

    public enum AirborneBossChildState
    {

        onflying,
        onfollow,
    }

        public enum OnGroundStateChildState
    {

        Vertigo,
        FlowerAttacking,
        NormalAttacking
    }

    [Header("空中状态参数")]
    public float flySpeed =10f ;
    public float flyHeight = 3f;//飞行高度
    public float airAttackInterval = 0.4f;//空中攻击间隔
    public float BoomAcornAttackInterval = 1f;//爆炸坚果攻击间隔

    public float patrolRange = 5f; // 左右巡逻范围

    public float trans2TreeStateTime = 10f;

    public Vector2 AirpatrolLeftBoundary; // 左侧巡逻边界
    public Vector2 AirpatrolRightBoundary; // 右侧巡逻边界

    public AirborneBossChildState airborneChildState;

    [Header("树上状态参数")]
    public float TreejumpForce;
    public float platformeDetectionRadius = 50f; // 检测周围平台的半径
    public float minJumpHeight = 2f;   // 最小跳跃高度
    public float maxJumpHeight = 4f;   // 最大跳跃高度
    public float jumpDuration = 1f;    // 跳跃持续时间(秒)
    public float minWaitTime = 1f;     // 最小等待时间(秒)
    public float maxWaitTime = 3f;    // 最大等待时间(秒)


    [Header("地面状态参数")]
    public float GroundjumpDuration = 2f;
    public float jumpHeight = 1f;
    public float EnterVertigotime = 2f;

    public float flowerAttackInterval = 2f; // 花攻击间隔
    public float flowerAttackShootInterval = 0.4f; // 花攻击射击子弹间隔时间
    public float normalAttackInterval = 2f; // 普通攻击间隔

    public float OnGroundStateMaxTime = 15f; // 地面状态最大持续时间
    private float OnGroundStateTimer = 0f; // 地面状态计时器

    public float AttackVertigoDuration = 2f; // 攻击后眩晕持续时间


    public OnGroundStateChildState onGroundChildState;

    [Header("地下状态参数")]
    public float diveTime;
    
    public float undergroundDeep = -10f; // 地下深度
    public float undergroundMoveSpeed = 5f; // 地下移动速度

    public float leafAttackStartTime = 1f; // 叶子攻击开始时间

    public float leafAttackInterval = 1f; // 叶子攻击间隔
    public float leafAttackMaxSpeed = 10f; // 叶子最大飞行速度
    public float leafAttackMinSpeed = 5f; // 叶子最小飞行速度
    public float leafAttackDuration = 4f; //叶子攻击持续时间
    public float leafSpawndistance = 20f; // 叶子生成距离

    public float GiantAcornDuration = 6f; // 巨大坚果开始时间
    public float GiantAcornInitHeight = 20f; // 巨大坚果生成高度

    public float UndergroundStateMaxTime = 6f; // 地下状态最大持续时间
    




    [Header("通用参数")]
    public bool isHitable;

    public Vector2 SpawnPos; // 出生位置



    private float attackTimer; // 攻击计时器

    public GameObject Target; // 目标对象
    public NPCBattleValueManager nbvm;
    public Rigidbody2D rb;
    private Animator animator;

    public Transform Etransform;

    public GameObject acornPrefab; // 松果预制体
    public GameObject boomAcornPrefab; // 爆炸坚果预制体
    public GameObject leafPrefab; // 叶子预制体
    public GameObject giantAcornPrefab; // 巨大坚果预制体
    public Transform acornSpawnPoint; // 松果发射点

    private float jumpProgress = 0f;

    public Collider2D fsbCollider; // 飞鼠碰撞体

    [Header("状态机")]

    public StateMachine stateMachine;
    public State airborneState;
    public State onTreeState;
    public State onGroundState;
    public State onGround_FlowerAttackState;
    public State onGround_normalAttackState;

    public State undergroundState;


       public void InitState()//初始化状态机相关参数
    {
        stateMachine=new StateMachine();
        airborneState =new AirborneState (this);
        onGroundState=new onGroundState(this);
        onGround_FlowerAttackState = new onGround_FlowerAttackState(this);
        onGround_normalAttackState = new onGround_normalAttackState(this);
        undergroundState= new undergroundState(this);
        onTreeState = new onTreeState(this);
        stateMachine.Init(airborneState);
        //stateMachine.Init(undergroundState);
    }
    void Start()
    {
        nbvm = gameObject.GetComponent<NPCBattleValueManager>();
        SpawnPos = transform.position;
        Etransform = transform;
        float currentflyHigh = Target.transform.position.y + flyHeight;
        AirpatrolLeftBoundary = new Vector2(transform.position.x, 0) + new Vector2(-patrolRange, currentflyHigh);
        AirpatrolRightBoundary = new Vector2(transform.position.x, 0) + new Vector2(patrolRange, currentflyHigh);
        
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        fsbCollider = gameObject.GetComponent<Collider2D>();
        InitState();
        
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Target.transform.position.x - transform.position.x;
        transform.localScale = new Vector2(-Mathf.Sign(direction) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        stateMachine.currentState.FrameUpdate();
        
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    #region 碰撞检测函数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    #endregion

    #region 功能函数

    public virtual void BoomAcornAttack()// 爆炸坚果攻击
    {
            // 创建松果
            GameObject boomAcorn = Object.Instantiate(boomAcornPrefab, acornSpawnPoint.position, Quaternion.identity);

            // 设置松果的旋转
            boomAcorn.GetComponent<Rigidbody2D>().angularVelocity = 500f; // 设置松果的旋转速度

            // 计算朝向玩家的方向
            Vector2 direction = (Target.transform.position - acornSpawnPoint.position).normalized;

            // 给松果添加力使其朝向玩家
            Rigidbody2D acornRigidbody = boomAcorn.GetComponent<Rigidbody2D>();
            acornRigidbody.gravityScale = 0f; // 取消重力
            if (acornRigidbody != null)
            {
                acornRigidbody.AddForce(direction * 8f, ForceMode2D.Impulse);
            }

    }

    
    public virtual void NormalAcornAttack()// 普通坚果攻击
    {
            // 创建松果
            GameObject Acorn = Object.Instantiate(acornPrefab, acornSpawnPoint.position, Quaternion.identity);

            // 设置松果的旋转
            Acorn.GetComponent<Rigidbody2D>().angularVelocity = 500f; // 设置松果的旋转速度

            // 计算朝向玩家的方向
            Vector2 direction = (Target.transform.position - acornSpawnPoint.position).normalized;

            // 给松果添加力使其朝向玩家
            Rigidbody2D acornRigidbody = Acorn.GetComponent<Rigidbody2D>();
            acornRigidbody.gravityScale = 0f; // 取消重力
            if (acornRigidbody != null)
            {
                acornRigidbody.AddForce(direction * 12f, ForceMode2D.Impulse);
            }

    }

    public virtual void FlowerAcornAttack()// 向四周发射普通坚果
    {
            // 向四周发射普通坚果
            for (int i = 0; i < 4; i++)
            {
                Vector2 direction = Quaternion.Euler(0f, 0f, 30f * i) * Vector2.right;
                Vector3 spawnPosition = acornSpawnPoint.position + new Vector3(direction.x, direction.y, 0f) * 2f;
                GameObject Acorn = Object.Instantiate(acornPrefab, spawnPosition, Quaternion.identity);
                Acorn.GetComponent<Rigidbody2D>().angularVelocity = 500f; // 设置松果的旋转速度
                Rigidbody2D acornRigidbody = Acorn.GetComponent<Rigidbody2D>();
                acornRigidbody.gravityScale = 0f; // 取消重力
                if (acornRigidbody != null)
                {
                    acornRigidbody.AddForce(direction * 5f, ForceMode2D.Impulse);
                }
            }
    }

    public virtual void GiantAcornAttack()
    {
        Vector2 InitPos = new Vector2 (Target.transform.position.x , Target.transform.position.y+ GiantAcornInitHeight);
        GameObject giantAcorn = Object.Instantiate(giantAcornPrefab, InitPos, Quaternion.identity);

        giantAcorn.GetComponent<Rigidbody2D>().angularVelocity = 500f; // 设置松果的旋转速度

        // 计算朝向玩家的方向
        Vector2 direction = (Target.transform.position - giantAcorn.transform.position).normalized;

        // 给松果添加力使其朝向玩家
        Rigidbody2D acornRigidbody = giantAcorn.GetComponent<Rigidbody2D>();
        acornRigidbody.gravityScale = 0f; // 取消重力
        if (acornRigidbody != null)
        {
            acornRigidbody.AddForce(direction * 2f, ForceMode2D.Impulse);
        }

    }

    public virtual void LeafAttack()//叶子攻击
    {
        int LeftOrRightDir = Random.Range(0, 2) == 0 ? -1 : 1;

        Vector2 InitPos = new Vector2 (Target.transform.position.x + LeftOrRightDir * leafSpawndistance, Target.transform.position.y);
        GameObject leaf = Object.Instantiate(leafPrefab, InitPos, Quaternion.identity);
        Vector2 direction = new Vector2(Target.transform.position.x - leaf.transform.position.x, 0f).normalized;

        Rigidbody2D leafRigidbody = leaf.GetComponent<Rigidbody2D>();
        leafRigidbody.gravityScale = 0f; // 取消重力
        if (leafRigidbody != null)
        {
            float leafAttackSpeed = Random.Range(leafAttackMinSpeed, leafAttackMaxSpeed);
            leafRigidbody.AddForce(direction * leafAttackSpeed, ForceMode2D.Impulse);
        }


    }


    public virtual void VertigoCount(float VertigoTime, float vertigoTimer)// 通用眩晕状态
    {

    }

    public virtual bool JumpToTarget(Vector2 targetPosition,Vector2 startPosition, bool isJumping)// 通用跳跃到目标
    {
        if (isJumping)
        {
            // 更新跳跃进度
            jumpProgress += Time.deltaTime / GroundjumpDuration;
            
            if (jumpProgress < 1f)
            {
                // 计算抛物线跳跃的当前位置
                float parabola = 1f - Mathf.Pow(2f * jumpProgress - 1f, 2f);
                
                Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, jumpProgress);
                newPosition.y += parabola * jumpHeight;
                
                transform.position = newPosition;
                return true;
            }
            else
            {
                // 跳跃完成
                transform.position = targetPosition;
                //BoomAcornAttack();
                jumpProgress = 0f;
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public virtual void OnGroundStateTimeCount()
    {
        if (OnGroundStateTimer < OnGroundStateMaxTime)
        {
            OnGroundStateTimer += Time.deltaTime;
        }
        else
        {
            stateMachine.ChangeState(undergroundState);
            OnGroundStateTimer = 0f;
        }
    }

    



    #endregion


}
