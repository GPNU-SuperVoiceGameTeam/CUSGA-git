using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class undergroundState : EnemyState
{
    public FlySquirrelBOSS flySquirrelBOSS;
    public undergroundState(Enemy enemy) : base(enemy)
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
