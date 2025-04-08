using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneState : EnemyState
{
    public FlySquirrelBOSS fsb;
    private Vector3 patrolLeftBoundary;
    private Vector3 patrolRightBoundary;
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
        if (fsb.airborneChildState == FlySquirrelBOSS.AirborneBossChildState.onflying)//飞行子状态
        {
            //Debug.Log("onflying");
            Patrol();
        }

    }

    public override void PhysicsUpdate()
    {
        
    }


    void Patrol()
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
}
