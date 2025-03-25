using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//生命值系统
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
        }
    }
    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHP -= damage;
            isHit = true;
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
