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
    public float jumpForce;

    [Header("地下状态参数")]
    public float diveTime;

    [Header("通用参数")]
    public bool isHitable;

    private float attackTimer; // 攻击计时器

    public GameObject Target; // 目标对象
    private NPCBattleValueManager nbvm;
    public Rigidbody2D rb;
    private Animator animator;

    public Transform Etransform;

    public GameObject acornPrefab; // 松果预制体
    public GameObject boomAcornPrefab; // 爆炸坚果预制体
    public Transform acornSpawnPoint; // 松果发射点

    [Header("状态机")]

    public StateMachine stateMachine;
    public State airborneState;
    public State onTreeState;
    public State onGroundState;

    public State undergroundState;


       public void InitState()//初始化状态机相关参数
    {
        stateMachine=new StateMachine();
        airborneState =new AirborneState (this);
        onGroundState=new onGroundState(this);
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



    #endregion


}
