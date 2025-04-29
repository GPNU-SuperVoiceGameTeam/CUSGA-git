using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2CatController : MonoBehaviour
{
    public int hasCollected = 0;
    private bool isEnter;
    public GameObject notEnough;
    public GameObject doorClose;
    public GameObject doorOpen;
    void Start()
    {
        notEnough.SetActive(false);
        doorClose.SetActive(true);
        doorOpen.SetActive(false);
    }
    void Update()
    {
        if(isEnter){
            if(Input.GetKeyDown(KeyCode.F)){
                if(hasCollected != 4){
                    notEnough.SetActive(true);
                }else{
                    doorClose.SetActive(false);
                    doorOpen.SetActive(true);
                }
            }
        }else{
            notEnough.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            isEnter = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            isEnter = false;
        }
    }
}
