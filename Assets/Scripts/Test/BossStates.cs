using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//生命值系统
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
        // 触发死亡动画和效果
    }
}
