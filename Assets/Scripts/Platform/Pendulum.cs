using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Transform pivotPoint; // �ڴ��Ĺ̶���
    public float swingAngle = 45f; // �ڶ������Ƕ�
    public float swingSpeed = 1f; // �ڶ����ٶ�

    private float initialAngle;
    private float currentAngle;

    void Start()
    {
        initialAngle = transform.eulerAngles.z;
        currentAngle = initialAngle;
    }

    void Update()
    {
        // ����ڶ��Ƕ�
        currentAngle = initialAngle + Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        // ���ðڴ�����ת
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        // ������ת���°ڴ���λ��
        Vector3 pivotPosition = pivotPoint.position;
        float radius = Vector3.Distance(transform.position, pivotPosition);
        float angleInRadians = currentAngle * Mathf.Deg2Rad;
        Vector3 newPosition = pivotPosition + new Vector3(Mathf.Sin(angleInRadians), -Mathf.Cos(angleInRadians), 0) * radius;
        transform.position = newPosition;
    }
}