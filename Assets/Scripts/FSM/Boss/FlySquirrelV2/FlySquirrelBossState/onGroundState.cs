using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGroundState : EnemyState
{
    /*三阶段，地面阶段：
    掉在地上后先发呆2s
    两种攻击模式
    Boss往主角位置跳跃，砸向主角。跳到目标位置后，向周围洒数颗松果。停止2s
    Boss往主角位置直直地水平丢一颗松果。停止2s
    这个阶段持续15s
    此时boss会扣血
    */
    public FlySquirrelBOSS fsb;
    private float vertigoTimer=0;
    public onGroundState(Enemy enemy) : base(enemy)
    {
        fsb=enemy as FlySquirrelBOSS;
    }

    public override void EnterState()
    {
        //Debug.Log("onGroundState");
        vertigoTimer=0;
        fsb.onGroundChildState = FlySquirrelBOSS.OnGroundStateChildState.Vertigo;
        

    }

    public override void ExitState()
    {

    }
    
    public override void FrameUpdate()
    {
        switch (fsb.onGroundChildState)
        {
            case FlySquirrelBOSS.OnGroundStateChildState.Vertigo:
            OnVertigoState(fsb.EnterVertigotime);
            return;

            case FlySquirrelBOSS.OnGroundStateChildState.FlowerAttacking:
            return;

            case FlySquirrelBOSS.OnGroundStateChildState.NormalAttacking:
            return;
            
        }
        fsb.OnGroundStateTimeCount();

    }

    public override void PhysicsUpdate()
    {
        
    }

    #region 功能函数
    void OnVertigoState(float VertigoTime)//这里可以补充眩晕动画的播放
    {
        if (vertigoTimer < VertigoTime)
        {
            vertigoTimer += Time.deltaTime;
        }
        else
        {
            //fsb.onGroundChildState = FlySquirrelBOSS.OnGroundStateChildState.FlowerAttacking;
            //fsb.onGroundChildState = Random.Range(0, 2) == 0 ? FlySquirrelBOSS.OnGroundStateChildState.FlowerAttacking : FlySquirrelBOSS.OnGroundStateChildState.NormalAttacking;
            //随机取状态
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                fsb.stateMachine.ChangeState(fsb.onGround_FlowerAttackState);
            }
            else
            {
                fsb.stateMachine.ChangeState(fsb.onGround_normalAttackState);
            }
        }
    

    }

    #endregion

    #region 状态携程函数

    #endregion
}
