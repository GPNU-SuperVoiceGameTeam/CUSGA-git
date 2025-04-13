using Unity.VisualScripting;
using UnityEngine;

public class CanPressF_Controller : MonoBehaviour
{
    public GameObject press;
    public PlayerController playerController;
    void Start()
    {
        press.SetActive(false);
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
