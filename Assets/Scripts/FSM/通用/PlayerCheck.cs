using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    public GameObject enemy;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            enemy.GetComponent<SquirrelStateMachine>().currentState = SquirrelStateMachine.State.Attack;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            enemy.GetComponent<SquirrelStateMachine>().currentState = SquirrelStateMachine.State.Patrol;
        }
    }
}
