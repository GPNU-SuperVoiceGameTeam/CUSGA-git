using UnityEngine;

public class GunUnlock : MonoBehaviour
{
    public BattleWaveVoicer battleWaveVoicer;
    public GameObject gun;
    public GameObject UI;
    public PlayerController playerController;
    public GameObject tip;
    public bool isEnter;
    void Update()
    {
        if(isEnter){
            if(Input.GetKeyDown(KeyCode.F)){
                battleWaveVoicer.Object[1].GetComponent<AudioSource>().Play();
                UI.SetActive(true);
                playerController.canAttack = true;
                playerController.lowWaveUnlock = true;
                playerController.highWaveUnlock = true;
                gun.SetActive(true);
                tip.SetActive(true);
                Destroy(gameObject);
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
