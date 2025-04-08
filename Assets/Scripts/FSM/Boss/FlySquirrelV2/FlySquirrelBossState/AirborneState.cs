using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class AirborneState : EnemyState
{
    public FlySquirrelBOSS fsb;
    private Vector3 patrolLeftBoundary;
    private Vector3 patrolRightBoundary;

    private float attackTimer = 0f;

    private bool state2followSwitch = false;
    private float state2followTimer = 0f;

    private float state2followMaxTime = 4f;


    private bool isMovingRight = true;
    public AirborneState(Enemy enemy) : base(enemy)
    {
        fsb=enemy as FlySquirrelBOSS;
    }

    public override void EnterState()
    {
        fsb.airborneChildState = FlySquirrelBOSS.AirborneBossChildState.onflying;
        patrolLeftBoundary = (Vector2)fsb.transform.position + new Vector2(-fsb.patrolRange, 0);
        patrolRightBoundary = (Vector2)fsb.transform.position + new Vector2(fsb.patrolRange, 0);


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
                state2followSwitch = true;
            }
            break;
            
            case FlySquirrelBOSS.AirborneBossChildState.onfollow:
            followPlayerPatrol();
            BoomAcornAttack();
            break;
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
