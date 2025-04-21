using UnityEngine;

public class WaterLevelController : MonoBehaviour
{
    public float dropSpeed = 0.01f;
    public float minYPosition = -1f;

    public void DecreaseWaterLevel()
    {
        if (transform.position.y > minYPosition)
        {
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
        }
    }
}