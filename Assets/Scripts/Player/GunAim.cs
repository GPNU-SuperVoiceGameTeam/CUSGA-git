using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    public Transform gunBone; // 枪的骨骼
    public float aimSpeed = 10f; // 旋转速度

    private Camera mainCamera; // 主摄像机引用

    void Start()
    {
        mainCamera = Camera.main; // 获取主摄像机
    }

    void Update()
    {
        // 获取鼠标在屏幕上的位置
        Vector2 mousePosition = Input.mousePosition;

        // 将屏幕坐标转换为世界坐标
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        // 计算鼠标相对于角色的位置
        Vector2 relativePosition = worldPosition - (Vector2)transform.position;

        // 计算枪的旋转角度
        float angle = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) < 90f)
        {
            gunBone.localScale = new Vector3(-1, 1, 1); // 镜像翻转
            angle = angle+180f; // 修正角度
        }
        else
        {
            gunBone.localScale = new Vector3(1, 1, 1); // 恢复正常
        }

        // 平滑旋转枪的骨骼
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        gunBone.rotation = Quaternion.Lerp(gunBone.rotation, targetRotation, aimSpeed * Time.deltaTime);
    }
}
