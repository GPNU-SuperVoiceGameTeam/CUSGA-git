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
            // ��������ҵ���ײ
            if (stateManager.currentState == BossState.Ground)
            {
                stateManager.currentState = BossState.TakingDamage;
            }
        }
    }

    // ������ײ����߼�...
}
