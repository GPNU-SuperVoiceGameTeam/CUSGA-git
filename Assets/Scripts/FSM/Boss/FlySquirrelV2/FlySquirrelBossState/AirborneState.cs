using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class AirborneState : EnemyState
{
    /*一阶段，空中阶段：（除空中阶段有螺旋桨，其余阶段去掉螺旋桨）
    在空中快速往下丢松果，一段时间后，自动下落到随机一棵树上
    前4s，来回移动，不跟随主角，每0.4s往下丢一颗普通松果，
    4s后，跟随主角x轴移动，每1s往下丢一颗爆炸松果，爆炸半径为1。持续6秒
    之后，降落到随机一颗树上
    总共10s
     */
    public FlySquirrelBOSS fsb;
    private Vector3 patrolLeftBoundary;
    private Vector3 patrolRightBoundary;

    private float attackTimer = 0f;
    private float trans2TreeStateTimer = 0f;
    private float state2followTimer = 0f;

    private float state2followMaxTime = 4f;

    private float currentflyHigh;


    private bool isMovingRight = true;
    public AirborneState(Enemy enemy) : base(enemy)
    {
        fsb=enemy as FlySquirrelBOSS;
    }

    public override void EnterState()
    {
        currentflyHigh = fsb.Target.transform.position.y + fsb.flyHeight;
        fsb.airborneChildState = FlySquirrelBOSS.AirborneBossChildState.onflying;
        patrolLeftBoundary = fsb.AirpatrolLeftBoundary;
        patrolRightBoundary = fsb.AirpatrolRightBoundary;
        fsb.rb.gravityScale = 0f; // 飞行状态重力为零
        fsb.nbvm.canTakeDamage = false; // 不能受伤

        attackTimer = 0f;
        trans2TreeStateTimer = 0f;
        state2followTimer = 0f;
        state2followMaxTime = 4f;


    }

    public override void ExitState()
    {
        

    }
    
    public override void FrameUpdate() 
    {
        switch (fsb.airborneChildState)
        {
            case FlySquirrelBOSS.AirborneBossChildState.onflying:
            NormalPatrol();
            acornAttack();
            state2followTimer += Time.deltaTime;
            if (state2followTimer >= state2followMaxTime)
            {
                fsb.airborneChildState = FlySquirrelBOSS.AirborneBossChildState.onfollow;
                
            }
            break;
            
            case FlySquirrelBOSS.AirborneBossChildState.onfollow:
            followPlayerPatrol();
            BoomAcornAttack();
            break;
        }

        if (trans2TreeStateTimer < fsb.trans2TreeStateTime)
        {
            trans2TreeStateTimer += Time.deltaTime;
        }
        else
        {
            fsb.stateMachine.ChangeState(fsb.onTreeState);
        }


    }

    public override void PhysicsUpdate()
    {

    }


    void NormalPatrol()//在两个点来回巡逻
    {
        // 正常行走巡逻
        if (isMovingRight)
        {

            // 向右移动
            fsb.transform.position = Vector2.MoveTowards(fsb.transform.position, patrolRightBoundary, fsb.flySpeed * Time.deltaTime);
            if (fsb.transform.position.x >= patrolRightBoundary.x)
            {
                isMovingRight = false; // 到达右边界，切换方向
            }
        }
        else
        {
            // 向左移动
            fsb.transform.position = Vector2.MoveTowards(fsb.transform.position, patrolLeftBoundary, fsb.flySpeed * Time.deltaTime);
            if (fsb.transform.position.x <= patrolLeftBoundary.x)
            {
                isMovingRight = true; // 到达左边界，切换方向
            }
        }
    }

    void followPlayerPatrol()//跟随玩家
    {
        Vector2 targetPosition = new Vector2(fsb.Target.transform.position.x, fsb.transform.position.y);
        fsb.transform.position = Vector2.MoveTowards(fsb.transform.position, targetPosition, fsb.flySpeed * Time.deltaTime);
        

    }


    void acornAttack()
    {
                // 检查是否达到攻击冷却时间
        if (attackTimer >= fsb.airAttackInterval)
        {
            // 创建松果
            GameObject acorn = Object.Instantiate(fsb.acornPrefab, fsb.acornSpawnPoint.position, Quaternion.identity);

            // 设置松果的旋转
            acorn.GetComponent<Rigidbody2D>().angularVelocity = 500f; // 设置松果的旋转速度

            // 计算朝向玩家的方向
            Vector2 direction = (fsb.Target.transform.position - fsb.acornSpawnPoint.position).normalized;

            // 给松果添加力使其朝向玩家
            Rigidbody2D acornRigidbody = acorn.GetComponent<Rigidbody2D>();
            acornRigidbody.gravityScale = 0f; // 取消重力
            if (acornRigidbody != null)
            {
                acornRigidbody.AddForce(direction * 10f, ForceMode2D.Impulse);
            }

            // 更新下一次攻击时间
            attackTimer = 0f;
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }

    void BoomAcornAttack()
    {

        if (attackTimer >= fsb.BoomAcornAttackInterval)
        {
            // 创建松果
            GameObject boomAcorn = Object.Instantiate(fsb.boomAcornPrefab, fsb.acornSpawnPoint.position, Quaternion.identity);

            // 设置松果的旋转
            boomAcorn.GetComponent<Rigidbody2D>().angularVelocity = 500f; // 设置松果的旋转速度

            // 计算朝向玩家的方向
            Vector2 direction = (fsb.Target.transform.position - fsb.acornSpawnPoint.position).normalized;

            // 给松果添加力使其朝向玩家
            Rigidbody2D acornRigidbody = boomAcorn.GetComponent<Rigidbody2D>();
            acornRigidbody.gravityScale = 0f; // 取消重力
            if (acornRigidbody != null)
            {
                acornRigidbody.AddForce(direction * 10f, ForceMode2D.Impulse);
            }

            // 更新下一次攻击时间
            attackTimer = 0f;
        }
        else
        {
            attackTimer += Time.deltaTime;
        }

    }
}
