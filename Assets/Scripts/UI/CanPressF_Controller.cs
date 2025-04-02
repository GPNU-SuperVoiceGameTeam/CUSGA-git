using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPressF_Controller : MonoBehaviour
{
    public GameObject press;
    public GameObject dialogue;
    void Start()
    {
        press.SetActive(false);
        dialogue.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)){
            dialogue.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            press.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            press.SetActive(false);
        }
    }
}
