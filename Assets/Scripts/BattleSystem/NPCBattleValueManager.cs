using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCBattleValueManager : MonoBehaviour
{
    public enum NPCType
    {
        None,
        Player,
        NormalEnemy_noCollision,
        NormalEnemy_withCollision,
        Boss
    }
    //储存数值的脚本，主角与NPC通用
    [Header("战斗数值")]
    public float MaxHP = 5;
    public float CurrentHP = 5;
    public int Attack = 1;

    public bool canTakeDamage = true;
    
    [Header("位移数值")]
    // public float MoveSpeed = 5;
    // public float JumpForce = 180;

    [Header("AI数值")]
    public float AIAttackInterval = 1;//攻击间隔

    [Header("阵营")]
    public NPCType npcType;

    #region 私有变量
    [Header("私有变量")]
    private Rigidbody2D rb;
    public bool isHit = false;

    #endregion

    private PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        
    }

    #region 碰撞检测函数

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (npcType)
        {
            case NPCType.Player://玩家碰撞检测
            return;

            case NPCType.NormalEnemy_noCollision://普通敌人碰撞检测
            if (collision.gameObject.CompareTag("lowWave"))
            {
                isHit = true;
                TakeDamage(1);
                Destroy(collision);
                Invoke("Enemy_InvokedIsHit", 0.2f);
            }
            return;
            
            case NPCType.NormalEnemy_withCollision://普通敌人碰撞检测
            if (collision.gameObject.CompareTag("lowWave")||collision.gameObject.CompareTag("highWave"))
            {
                isHit = true;
                TakeDamage(1);
                Destroy(collision);
                Invoke("Enemy_InvokedIsHit", 0.2f);
            }else if(collision.gameObject.CompareTag("Player")){
                player.TakeDamage(1);
            }
            return;

            case NPCType.Boss://Boss碰撞检测
            return;

            default:
            return;
        }
    }

        private void OnTriggerExit2D(Collider2D collision)
    {
        switch (npcType)
        {
            case NPCType.Player://玩家碰撞检测
            return;

            case NPCType.NormalEnemy_noCollision://普通敌人碰撞检测
            if (collision.gameObject.CompareTag("lowWave"))
            {
                isHit = false;
            }
            return;

            case NPCType.NormalEnemy_withCollision://普通敌人碰撞检测
            if (collision.gameObject.CompareTag("lowWave"))
            {
                isHit = false;
            }
            return;

            case NPCType.Boss://Boss碰撞检测
            return;

            default:
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (npcType)
        {
            case NPCType.Player://玩家碰撞检测
            return;

            case NPCType.NormalEnemy_noCollision://普通敌人碰撞检测
            return;

            case NPCType.Boss://Boss碰撞检测
            return;

            default:
            return;
        }
    }

    #endregion




    #region 功能函数

    #region 共有函数
    public void DeathJudge()
    {
        if (CurrentHP <= 0)
        {
            if (npcType == NPCType.Player)
            {
                Player_Death();
            }
            else
            {
                Enemy_Die();
            }
        }
    }

    #region 普通敌人函数区域
    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            CurrentHP -= damage;
            isHit = true;
        }
        if (CurrentHP <= 0)
        {
            Enemy_Die();
        }
    }

        void Enemy_Die()
    {
        Destroy(gameObject);
    }

    void Enemy_InvokedIsHit()
    {
        isHit = false;
    }
    
    #endregion
    #region 玩家函数区域

    private void Player_Death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        CameraFollow camera = Camera.main.GetComponent<CameraFollow>();
    
        if (camera != null)
        {
            //camera.StopFollowing();
        }
    }

    private void Restart()//死亡后重新加载场景
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }



    #endregion
    #endregion
    #endregion
}
