using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ֵϵͳ
public class BossStates : MonoBehaviour
{
    public int maxHP = 5;
    public int currentHP;
    public bool canTakeDamage;
    public bool isHit = false;

    void Start()
    {
        currentHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wave"))
        {
            isHit = true;
            Destroy(collision);
        }
        else
        {
            isHit = false;
        }
    }
    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHP -= damage;
            isHit = true;
            canTakeDamage = false;
        }
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
