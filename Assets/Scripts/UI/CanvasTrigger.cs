using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    public PlayerController player;
    public GameObject tip;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            tip.SetActive(false);
            player.canMove = true;
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        tip.SetActive(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.canMove = false;
    }
}
