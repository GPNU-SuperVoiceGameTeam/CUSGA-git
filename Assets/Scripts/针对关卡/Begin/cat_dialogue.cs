using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_dialogue : MonoBehaviour
{
    public GameObject dialogue;
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            if(Input.GetKeyDown(KeyCode.F)){
                dialogue.SetActive(true);
                }
            }
    }
}
