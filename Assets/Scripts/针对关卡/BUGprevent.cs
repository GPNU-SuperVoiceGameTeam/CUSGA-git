using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUGprevent : MonoBehaviour
{
    public PlayerController player;
    public bool isEnter = false;
    public GameObject pos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnter){
            player.transform.position = pos.transform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
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
