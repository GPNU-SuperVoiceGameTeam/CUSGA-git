using UnityEngine;

public class before_beginCamera : MonoBehaviour
{
    public Transform player; // 拖拽主角的 Transform 组件
    public float offsetX = 0f; // 相机在 X 轴上的偏移量
    public float offsetY = 0f; // 相机在 Y 轴上的偏移量

    void LateUpdate()
    {
        // 保持相机的 X 轴位置不变，只跟随主角的 Y 轴位置
        transform.position = new Vector3(player.transform.position.x, transform.position.y + offsetY, transform.position.z);
    }
}
