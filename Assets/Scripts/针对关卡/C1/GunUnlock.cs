using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUnlock : MonoBehaviour
{
    public GameObject gun;
    public GameObject UI;
    public PlayerController playerController;
    public GameObject tip;
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.F)){
                UI.SetActive(true);
                playerController.canAttack = true;
                playerController.lowWaveUnlock = true;
                playerController.highWaveUnlock = true;
                gun.SetActive(true);
                tip.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
