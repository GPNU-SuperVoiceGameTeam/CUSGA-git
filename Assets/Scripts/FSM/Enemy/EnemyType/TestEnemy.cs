using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    

    [Header("状态机")]
    public StateMachine stateMachine;
    public State followState;
    public State patrolState;
    void Start()
    {
        InitState();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitState()
    {
        stateMachine=new StateMachine();
        followState=new FollowState(this);
        
        stateMachine.Init(followState);
    
    }
}
