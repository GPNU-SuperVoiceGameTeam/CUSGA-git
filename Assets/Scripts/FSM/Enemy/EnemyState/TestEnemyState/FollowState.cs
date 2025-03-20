using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : EnemyState
{
    public TestEnemy testEnemy;
    public FollowState(Enemy enemy) : base(enemy)
    {
        testEnemy=enemy as TestEnemy;
    }
    // Start is called before the first frame update
    public override void EnterState()
    {
        
        
    }

    // Update is called once per frame
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
