using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public WaterLevelController waterLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//¸ü¸ÄÎªÉù²¨
        {
            waterLevel.DecreaseWaterLevel();
        }
    }
}