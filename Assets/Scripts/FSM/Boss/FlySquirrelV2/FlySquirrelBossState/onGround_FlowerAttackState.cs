using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGround_FlowerAttackState : EnemyState
{
    public FlySquirrelBOSS fsb;
    public bool isJumping;
    private bool notAttackSwitch = true;

    private bool StateComplete = false;

    private Vector2 tempTargetPos;
    // Boss往主角位置跳跃，砸向主角。跳到目标位置后，向周围洒数颗松果。停止2s

    public onGround_FlowerAttackState(Enemy enemy) : base(enemy)
    {
        fsb=enemy as FlySquirrelBOSS;
    }
    public override void EnterState()
    {
        //StateComplete = false;
        isJumping = true;
        tempTargetPos = fsb.Target.transform.position;
        
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        //Debug.Log(tempTargetPos);
        notAttackSwitch = isJumping = fsb.JumpToTarget(tempTargetPos, isJumping);
        if (!notAttackSwitch)
        {
            //fsb.FlowerAcornAttack();
            fsb.BoomAcornAttack();
            fsb.stateMachine.ChangeState(fsb.onGroundState);

        }
        fsb.OnGroundStateTimeCount();

    }

    public override void PhysicsUpdate()
    {

    }
    public void StateChange()
    {
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
