using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TengwanController : MonoBehaviour
{
    public PlayerController player;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            player.TakeDamage(1);
        }
    }
}
