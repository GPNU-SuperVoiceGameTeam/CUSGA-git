using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collections : MonoBehaviour
{
    private bool isEnter;
    public C2CatController catController;
    void Update()
    {
        if(isEnter){
            if(Input.GetKeyDown(KeyCode.F)){
                catController.hasCollected += 1;
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            isEnter = true;
        }
    }
}
