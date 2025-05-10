using System.Collections;
using UnityEngine;

public class CampfireController : MonoBehaviour
{
    public PlayerController player;
    public bool isEnter;
    public RebornPoint rebornPoint;
    public GameObject audioClip;
    public GameObject rebornText;
    void Start()
    {
        rebornText.SetActive(false);
    }
    void Update()
    {
        if(isEnter){
            if(Input.GetKeyDown(KeyCode.F)){
                audioClip.GetComponent<AudioSource>().Play();
                player.health = player.maxHealth;
                rebornPoint.spawnPoint.position = transform.position;
                StartCoroutine(RebornText(3.0f));
            }
        }
    }
    private IEnumerator RebornText(float duration){
        rebornText.SetActive(true);
        yield return new WaitForSeconds(duration);
        rebornText.SetActive(false);
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
