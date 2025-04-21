using UnityEngine;

public class CampfireController : MonoBehaviour
{
    public PlayerController player;
    public bool isEnter;
    void Update()
    {
        if(isEnter){
            if(Input.GetKeyDown(KeyCode.F)){
                player.health = player.maxHealth;
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
