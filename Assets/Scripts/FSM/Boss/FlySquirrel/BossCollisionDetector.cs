using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionDetector : MonoBehaviour
{
    private BossStateManager stateManager;
    private BossStates bossStates;

    void Start()
    {
        stateManager = GetComponent<BossStateManager>();
        bossStates = GetComponent<BossStates>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��������ҵ���ײ
            if (stateManager.currentState == BossState.Ground)
            {
                stateManager.currentState = BossState.TakingDamage;
            }
        }
        if (collision.gameObject.CompareTag("Wave"))
        {
            if (bossStates.canTakeDamage)
            {
                bossStates.TakeDamage(1);
            }
        }
    }

    // ������ײ����߼�...
}
