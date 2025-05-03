using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public GameObject spawnPoint;
    PlayerController playerController;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(1);
            if(playerController.health > 0){
                collision.transform.position = spawnPoint.transform.position;
            }
        }
    }
}
