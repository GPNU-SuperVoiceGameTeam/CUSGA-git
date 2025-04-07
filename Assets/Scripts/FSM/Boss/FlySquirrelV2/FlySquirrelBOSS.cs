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

    [Header("空中状态参数")]
    public float flySpeed =10f ;

    [Header("地面状态参数")]
    public float jumpForce;

    [Header("地下状态参数")]
    public float diveTime;

    [Header("通用参数")]
    public bool isHitable;
    private NPCBattleValueManager nbvm;
    private Rigidbody2D rb;
    private Animator animator;

    [Header("状态机")]

    public StateMachine stateMachine;
    public State airborneState;
    public State onGroundState;

    public State undergroundState;


       public void InitState()//初始化状态机相关参数
    {
        stateMachine=new StateMachine();
        //airborneState =new AirborneState (this);
        //onGroundState=new onGroundState(this);
        //undergroundState= new undergroundState(this);
        stateMachine.Init(airborneState);
    }
    void Start()
    {
        nbvm = GetComponent<NPCBattleValueManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
