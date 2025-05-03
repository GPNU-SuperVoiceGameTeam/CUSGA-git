using UnityEngine;

public class JumpWave_get : MonoBehaviour
{
    public BattleWaveVoicer battleWaveVoicer;
    public PlayerController playerController;
    public GameObject tip;
    public Item item;
    public bool isEnter;

    void Update()
    {
        if(isEnter){
            if(Input.GetKeyDown(KeyCode.F)){
                battleWaveVoicer.Object[1].GetComponent<AudioSource>().Play();
                playerController.jumpWaveUnlock = true;
                item.isUnlocked = true;
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
