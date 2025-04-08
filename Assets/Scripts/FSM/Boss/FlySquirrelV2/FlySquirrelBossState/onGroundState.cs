using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGroundState : EnemyState
{
    public FlySquirrelBOSS flySquirrelBOSS;
    public onGroundState(Enemy enemy) : base(enemy)
    {
        flySquirrelBOSS=enemy as FlySquirrelBOSS;
    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }
    
    public override void FrameUpdate()
    {

    }

    public override void PhysicsUpdate()
    {
        
    }
}
