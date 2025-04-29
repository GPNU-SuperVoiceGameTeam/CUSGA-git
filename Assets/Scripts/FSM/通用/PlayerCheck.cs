using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    public enum EnemyName{
        Squirrel,
        Spider
    }
    public EnemyName enemyName;
    public GameObject enemy;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            switch(enemyName){
                case EnemyName.Squirrel:
                    enemy.GetComponent<SquirrelStateMachine>().currentState = SquirrelStateMachine.State.Attack;
                    break;
                case EnemyName.Spider:
                    break;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            switch(enemyName){
                case EnemyName.Squirrel:
                    enemy.GetComponent<SquirrelStateMachine>().currentState = SquirrelStateMachine.State.Patrol;
                    break;
                case EnemyName.Spider:
                    enemy.GetComponent<SpiderStateMachine>().currentState = SpiderStateMachine.State.Patrol;
                    break;
            }
        }
    }
}
