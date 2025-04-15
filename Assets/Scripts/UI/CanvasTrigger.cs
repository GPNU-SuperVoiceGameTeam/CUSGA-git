using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    public PlayerController player;
    public GameObject[] tip;
    private int tipIndex = 0;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            if(tipIndex + 1 >= tip.Length){
                for(int i = 0; i < tip.Length; i++){
                    tip[i].SetActive(false);
                }
                player.canMove = true;
                gameObject.SetActive(false);
            }else{
                tip[tipIndex].SetActive(false);
                tipIndex++;
                tip[tipIndex].SetActive(true);
            }
            //Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        tip[tipIndex].SetActive(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.canMove = false;
    }
}
