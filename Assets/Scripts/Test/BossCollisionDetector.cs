using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionDetector : MonoBehaviour
{
    private BossStateManager stateManager;

    void Start()
    {
        stateManager = GetComponent<BossStateManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 处理与玩家的碰撞
            if (stateManager.currentState == BossState.Ground)
            {
                stateManager.currentState = BossState.TakingDamage;
            }
        }
    }

    // 其他碰撞检测逻辑...
}
