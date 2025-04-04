using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//加载场景需要写
//角色死亡
public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    //角色动画 private Animator anim;
    //粒子系统  public GameObjet playerPS;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //动画anim = GetComponent<Animator>();   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="dieline")
        {
            Invoke("Restart",0.4f);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Death();
            Restart();
        }
    }

    private void Death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        CameraFollow camera = Camera.main.GetComponent<CameraFollow>();
    
        if (camera != null)
        {
            //camera.StopFollowing();
        }

        //动画anim.SetTrigger("//标签death");
        // 粒子系统 Destroy(playerPS,1f);
    }
    private void Restart()//死亡后重新加载场景
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
  
