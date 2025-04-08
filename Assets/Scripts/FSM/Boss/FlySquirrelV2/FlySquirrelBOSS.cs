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
    public float patrolRange = 5f; // 左右巡逻范围

    public AirborneBossChildState airborneChildState;

    [Header("树上状态参数")]
    public float TreejumpForce;


    [Header("地面状态参数")]
    public float jumpForce;

    [Header("地下状态参数")]
    public float diveTime;

    [Header("通用参数")]
    public bool isHitable;
    private NPCBattleValueManager nbvm;
    private Rigidbody2D rb;
    private Animator animator;

    public Transform Etransform;

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
        nbvm = GetComponent<NPCBattleValueManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
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



    #endregion


}
