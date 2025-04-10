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
    public float airAttackInterval = 0.4f;//空中攻击间隔
    public float BoomAcornAttackInterval = 1f;//爆炸坚果攻击间隔

    public float patrolRange = 5f; // 左右巡逻范围

    public float trans2TreeStateTime = 10f;

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
    public float normalAttackInterval = 2f; // 普通攻击间隔

    public float OnGroundStateMaxTime = 15f; // 地面状态最大持续时间
    private float OnGroundStateTimer = 0f; // 地面状态计时器

    public float AttackVertigoDuration = 2f; // 攻击后眩晕持续时间


    public OnGroundStateChildState onGroundChildState;

    [Header("地下状态参数")]
    public float diveTime;

    [Header("通用参数")]
    public bool isHitable;

    private float attackTimer; // 攻击计时器

    public GameObject Target; // 目标对象
    public NPCBattleValueManager nbvm;
    public Rigidbody2D rb;
    private Animator animator;

    public Transform Etransform;

    public GameObject acornPrefab; // 松果预制体
    public GameObject boomAcornPrefab; // 爆炸坚果预制体
    public Transform acornSpawnPoint; // 松果发射点

    private bool isJumping = false;
    private float jumpProgress = 0f;

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
    }
    void Start()
    {
        Etransform = transform;
        InitState();
        nbvm = gameObject.GetComponent<NPCBattleValueManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
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
                acornRigidbody.AddForce(direction * 10f, ForceMode2D.Impulse);
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
                acornRigidbody.AddForce(direction * 10f, ForceMode2D.Impulse);
            }

    }


    public virtual void VertigoCount(float VertigoTime, float vertigoTimer)// 通用眩晕状态
    {

    }

    public virtual bool JumpToTarget(Vector2 targetPosition, bool isJumping)// 通用跳跃到目标
    {
        if (isJumping)
        {
            // 更新跳跃进度
            jumpProgress += Time.deltaTime / GroundjumpDuration;
            
            if (jumpProgress < 1f)
            {
                // 计算抛物线跳跃的当前位置
                float parabola = 1f - Mathf.Pow(2f * jumpProgress - 1f, 2f);
                
                Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, jumpProgress);
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
