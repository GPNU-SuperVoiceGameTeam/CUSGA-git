using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /*两个形态：
    1.常规型，跟着玩家运动，镜头大小较小
    2.关卡型，移动到关卡中心，镜头大小较大*/
    public Transform playerTransform; // 玩家的Transform组件
    private GameObject playerObject;
    public SpriteRenderer playerSpriteRenderer; // 玩家的SpriteRenderer组件
    public Vector3 offsetRight = new Vector3(2.3f, 1, -10); // 玩家面对右边时相机的相对位置
    public Vector3 offsetLeft = new Vector3(-2.3f, 1, -10); // 玩家面对左边时相机的相对位置
    public float movementTime = 10f;
   
    public bool LevelCarmeraMode = false;
    public bool FocusMode = false;
    public Transform FocusTarget;
    public float moveSpeed = 1;

    /* 新增跟随状态标志 private bool isFollowing = true;playerlife 死亡部分 摄像机静止 */

    void Awake()
    {
        
    }

    void Update()
    {
        /*if (!isFollowing) return; 如果停止跟随，直接退出更新逻辑playerlife 死亡部分 摄像机静止*/
        if (!FocusMode)
        {
            if(!LevelCarmeraMode)
            {
                if (playerSpriteRenderer.flipX == false)
                {
                // 玩家面对右边，相机移动到玩家的右侧
                transform.position = Vector3.Lerp(transform.position, playerTransform.position + offsetRight, Time.deltaTime * movementTime);
                }
                else
                {
                // 玩家面对左边，相机移动到玩家的左侧
                transform.position = Vector3.Lerp(transform.position, playerTransform.position + offsetLeft, Time.deltaTime * movementTime);
                }
            }
        }
        else
        {
            FocusOnObject();
        }

    }
    /*public void StopFollowing()
    {
        isFollowing = false; // 停止跟随
    }playerlife 死亡部分 摄像机静止*/
    public void FocusOnObject()//聚焦在某个物体上面
    {
        Vector3 targetPosition = new Vector3(FocusTarget.position.x, FocusTarget.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
