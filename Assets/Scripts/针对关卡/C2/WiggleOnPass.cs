using UnityEngine;

public class WiggleOnPass : MonoBehaviour
{
    [SerializeField] private float animationDuration = 0.8f; // 每段动画持续时间

    private enum WiggleState { Idle, ToMinus10, ToPlus10, BackToZero }
    private WiggleState currentState = WiggleState.Idle;

    private float timer = 0f;
    private Quaternion initialRotation;

    private bool hasPlayed = false; // 是否已经播放过一次动画

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            currentState = WiggleState.ToMinus10;
            hasPlayed = true;
        }
    }

    void Update()
    {
        if (currentState == WiggleState.Idle) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / animationDuration); // 插值比例 [0, 1]

        switch (currentState)
        {
            case WiggleState.ToMinus10:
                transform.rotation = Quaternion.Euler(Vector3.Lerp(initialRotation.eulerAngles, new Vector3(0, 0, -10), t));
                if (t >= 1f)
                {
                    timer = 0;
                    currentState = WiggleState.ToPlus10;
                }
                break;

            case WiggleState.ToPlus10:
                transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, -10), new Vector3(0, 0, 10), t));
                if (t >= 1f)
                {
                    timer = 0;
                    currentState = WiggleState.BackToZero;
                }
                break;

            case WiggleState.BackToZero:
                transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, 10), initialRotation.eulerAngles, t));
                if (t >= 1f)
                {
                    currentState = WiggleState.Idle;
                }
                break;
        }
    }
}