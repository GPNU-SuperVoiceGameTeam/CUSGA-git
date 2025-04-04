using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//���س�����Ҫд
//��ɫ����
public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    //��ɫ���� private Animator anim;
    //����ϵͳ  public GameObjet playerPS;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //����anim = GetComponent<Animator>();   
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

        //����anim.SetTrigger("//��ǩdeath");
        // ����ϵͳ Destroy(playerPS,1f);
    }
    private void Restart()//���������¼��س���
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
  
