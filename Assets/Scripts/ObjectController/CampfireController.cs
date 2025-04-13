using UnityEngine;

public class CampfireController : MonoBehaviour
{
    public PlayerController player;
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.F)){
                player.health = player.maxHealth;
            }
        }
    }
}
