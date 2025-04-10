using UnityEngine;

public class WaterLevelController : MonoBehaviour
{
    public float dropSpeed = 1f;
    public float minYPosition = -5f;

    public void DecreaseWaterLevel()
    {
        if (transform.position.y > minYPosition)
        {
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
        }
    }
}