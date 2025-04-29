using Fungus;
using UnityEngine;

public class WebSpeedDecrease : MonoBehaviour
{
    public bool isEnter;
    private PlayerController player;
    float originalMoveSpeed;
    float originalUpGravity;
    float originalDownGravity;
    float originalJumpForce;

    void Start()
    {
        ChangePlayerSpeed(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        originalMoveSpeed = player.moveSpeed;
        originalUpGravity = player.upGravity;
        originalDownGravity = player.downGravity;
        originalJumpForce = player.jumpForce;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("highWave"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            isEnter = false;
            ChangePlayerSpeed(false);
        }
        else if (other.CompareTag("Platform"))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    // void OnTriggerStay2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //     {
    //         isEnter = true;
    //         ChangePlayerSpeed(true);
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         isEnter = false;
    //         ChangePlayerSpeed(false);
    //     }
    // }

    void ChangePlayerSpeed(bool isInSlowZone)
    {
        if (player == null) return;

        if (isInSlowZone)
        {
            player.moveSpeed = 1;
            player.upGravity = 5;
            player.downGravity = 0.01f;
            player.jumpForce = 5;
        }
        else
        {
            player.moveSpeed = originalMoveSpeed;
            player.upGravity = originalUpGravity;
            player.downGravity = originalDownGravity;
            player.jumpForce = originalJumpForce;
        }
    }

    void OnDisable()
    {
        if (isEnter)
        {
            isEnter = false;
            ChangePlayerSpeed(false);
        }
    }
}
