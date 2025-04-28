using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform far, middle, front;
    private Vector2 lastPos;
    private Transform cam;

    [Tooltip("远景移动速度比例 (0-1)")]
    public float farParallaxFactor = 0.5f;
    [Tooltip("中景移动速度比例 (0-1)")]
    public float middleParallaxFactor = 0.7f;
    [Tooltip("前景移动速度比例 (0-1)")]
    public float frontParallaxFactor = 0.9f;

    void Start()
    {
        cam = Camera.main.transform;
        lastPos = cam.position;
    }

    void Update()
    {
        Vector2 positionChange = (Vector2)cam.position - lastPos;

        // 远景移动最慢
        if (far != null)
            far.position += (Vector3)(positionChange * farParallaxFactor);

        // 中景移动中等速度
        if (middle != null)
            middle.position += (Vector3)(positionChange * middleParallaxFactor);

        // 前景移动最快
        if (front != null)
            front.position += (Vector3)(positionChange * frontParallaxFactor);

        lastPos = cam.position;
    }
}