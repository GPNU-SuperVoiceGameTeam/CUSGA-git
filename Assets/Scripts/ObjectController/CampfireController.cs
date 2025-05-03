using UnityEngine;

public class CampfireController : MonoBehaviour
{
    public PlayerController player;
    public bool isEnter;
    public RebornPoint rebornPoint;
    void Update()
    {
        if(isEnter){
            if(Input.GetKeyDown(KeyCode.F)){
                player.health = player.maxHealth;
                rebornPoint.spawnPoint.position = transform.position;
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            isEnter = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            isEnter = false;
        }
    }
}
