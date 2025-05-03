using UnityEngine;

public class VineTrigger : MonoBehaviour
{
    
    private enum VineState {
         Retracted, 
         Extended 
    }
    private VineState state = VineState.Retracted;
    
    private float timer = 0f;

    public float extendDuration = 1.0f; // 突出持续时间
    public float retractWaitTime = 2.0f; // 缩回后等待时间
    public PlayerController player;

    void Update()
    {
        timer += Time.deltaTime;

        switch (state)
        {
            case VineState.Retracted:
                // 如果处于缩回状态，并且等待时间已到，开始突出
                if (timer >= retractWaitTime)
                {
                    // 立即设置为突出状态
                    gameObject.transform.localScale = new Vector2(1, -1.5f);
                    timer = 0;
                    gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                    state = VineState.Extended;
                }
                break;

            case VineState.Extended:
                // 突出状态下保持一段时间后缩回
                if (timer >= extendDuration)
                {
                    // 立即设置为缩回状态
                    gameObject.transform.localScale = new Vector2(1, -0.6f);
                    timer = 0;
                    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    state = VineState.Retracted;
                }
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            player.TakeDamage(1);
        }
    }
}