using UnityEngine;
using UnityEngine.Playables;

public class JumpWave_get : MonoBehaviour
{
    public PlayerController playerController;

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            if(Input.GetKeyDown(KeyCode.F)){
                playerController.jumpWaveUnlock = true;
                Destroy(gameObject);
            }
        }
    }

}
