using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEggTrigger : MonoBehaviour
{
    public GameObject[] spiderEgg;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            foreach(GameObject egg in spiderEgg){
                egg.SetActive(true);
            }
        }
    }
}
