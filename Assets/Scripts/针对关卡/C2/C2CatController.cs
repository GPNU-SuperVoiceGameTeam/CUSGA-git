using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2CatController : MonoBehaviour
{
    public int hasCollected = 0;
    private bool isEnter;
    public BattleWaveVoicer battleWaveVoicer;
    public GameObject notEnough;
    public GameObject doorClose;
    public GameObject doorOpen;
    public AudioSource cat;
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
                cat.Play();
                if(hasCollected != 4){
                    notEnough.SetActive(true);
                }else{
                    doorClose.SetActive(false);
                    battleWaveVoicer.Object[0].GetComponent<AudioSource>().Play();
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
