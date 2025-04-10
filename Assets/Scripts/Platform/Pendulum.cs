using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Transform pivotPoint; // 摆锤的固定点
    public float swingAngle = 45f; // 摆动的最大角度
    public float swingSpeed = 1f; // 摆动的速度

    private float initialAngle;
    private float currentAngle;

    void Start()
    {
        initialAngle = transform.eulerAngles.z;
        currentAngle = initialAngle;
    }

    void Update()
    {
        // 计算摆动角度
        currentAngle = initialAngle + Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        // 设置摆锤的旋转
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        // 根据旋转更新摆锤的位置
        Vector3 pivotPosition = pivotPoint.position;
        float radius = Vector3.Distance(transform.position, pivotPosition);
        float angleInRadians = currentAngle * Mathf.Deg2Rad;
        Vector3 newPosition = pivotPosition + new Vector3(Mathf.Sin(angleInRadians), -Mathf.Cos(angleInRadians), 0) * radius;
        transform.position = newPosition;
    }
}