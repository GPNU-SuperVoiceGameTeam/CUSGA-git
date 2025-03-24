using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ֵϵͳ
public class BossStates : MonoBehaviour
{
    public int maxHP = 5;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ��������������Ч��
    }
}
