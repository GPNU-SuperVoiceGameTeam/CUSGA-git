using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorClose : MonoBehaviour
{
    public GameObject doorOpen;
    public GameObject doorClose;
    void Start()
    {
        doorClose.SetActive(false);
        doorOpen.SetActive(true);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            doorOpen.SetActive(false);
            doorClose.SetActive(true);
        }
    }
}
