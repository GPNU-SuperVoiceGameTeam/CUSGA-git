using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform far, middle, front;
    private Vector2 lastPos;
    private Transform cam;

    [Tooltip("Զ���ƶ��ٶȱ��� (0-1)")]
    public float farParallaxFactor = 0.5f;
    [Tooltip("�о��ƶ��ٶȱ��� (0-1)")]
    public float middleParallaxFactor = 0.7f;
    [Tooltip("ǰ���ƶ��ٶȱ��� (0-1)")]
    public float frontParallaxFactor = 0.9f;

    void Start()
    {
        cam = Camera.main.transform;
        lastPos = cam.position;
    }

    void Update()
    {
        Vector2 positionChange = (Vector2)cam.position - lastPos;

        // Զ���ƶ�����
        if (far != null)
            far.position += (Vector3)(positionChange * farParallaxFactor);

        // �о��ƶ��е��ٶ�
        if (middle != null)
            middle.position += (Vector3)(positionChange * middleParallaxFactor);

        // ǰ���ƶ����
        if (front != null)
            front.position += (Vector3)(positionChange * frontParallaxFactor);

        lastPos = cam.position;
    }
}