using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class undergroundState : EnemyState
{
    /*四阶段，地下阶段：
    Boss躲进地下，屏幕两端会有飞叶袭来，持续4秒。
    每0.5s，随机在左边或者右边，往主角y轴位置发射一片飞叶（可以提前y轴标红线提示？）
    4秒后，一颗巨大的松果朝主角位置掉下来。Boss从屏幕顶部下来。
    重复空中阶段。
    */
    private Vector2 undergroundPos;
    public FlySquirrelBOSS fsb;

    private float timer;
    private bool isLeafAttacking = false;
    private bool hasGiantAttacked = false;

    private Coroutine leafAttackCoroutine;
    public undergroundState(Enemy enemy) : base(enemy)
    {
        fsb=enemy as FlySquirrelBOSS;
    }

    public override void EnterState()
    {
        //取消与地面的碰撞
        fsb.fsbCollider.enabled = false;
        fsb.rb.gravityScale = 0f; // 飞行状态重力为零
        hasGiantAttacked = false;
        fsb.nbvm.canTakeDamage = false; // 无法受伤
        isLeafAttacking = false;
        timer = 0f;

    }

    public override void ExitState()
    {
        //结束后，松鼠从上方出现
        fsb.transform.position = new Vector2(fsb.SpawnPos.x, fsb.SpawnPos.y + 2*fsb.flyHeight);
        fsb.StopCoroutine(leafAttackCoroutine);
        fsb.fsbCollider.enabled = true;
        timer = 0f;

    }
    
    public override void FrameUpdate()
    {
        UndergroundMoving();

        if(timer<fsb.UndergroundStateMaxTime)
        {
            timer += Time.deltaTime;
            if(timer>=fsb.leafAttackStartTime&&!isLeafAttacking)
            {
                //开始叶子携程
                isLeafAttacking = true;
                leafAttackCoroutine =fsb.StartCoroutine(continueLeafAttack());

            }
            if(timer>=fsb.GiantAcornDuration &&!hasGiantAttacked)
            {
                fsb.StopCoroutine(leafAttackCoroutine);
                //开始巨型果实攻击
                hasGiantAttacked = true;
                fsb.GiantAcornAttack();
            }
        }
        else
        {
            //地下状态结束，转到空中阶段
            fsb.stateMachine.ChangeState(fsb.airborneState);
        }

    }

    public override void PhysicsUpdate()
    {
        
    }

    void UndergroundMoving()//飞到地下，并跟随玩家移动
    {
        undergroundPos = new Vector2(fsb.Target.transform.position.x, fsb.Target.transform.position.y + fsb.undergroundDeep);
        fsb.transform.position = Vector2.MoveTowards(fsb.transform.position, undergroundPos, fsb.undergroundMoveSpeed  * Time.deltaTime);

    }

    private IEnumerator continueLeafAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(fsb.leafAttackInterval);
            fsb.LeafAttack();
        }
    }

}
