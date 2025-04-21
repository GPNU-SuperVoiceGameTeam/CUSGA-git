using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public bool isEnter;
    public GameObject water;
    public WaterLevelController waterLevel;
    public Sprite upSprite;
    public Sprite downSprite;
    void Update()
    {
        if(isEnter && water.transform.position.y > waterLevel.minYPosition){
            waterLevel.DecreaseWaterLevel();
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = true;
            GetComponent<SpriteRenderer>().sprite = downSprite;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = false;
            GetComponent<SpriteRenderer>().sprite = upSprite;
        }
    }
}