using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    public Transform player;
    public Vector2 mapMin;  // ��ͼ���½�����
    public Vector2 mapMax;  // ��ͼ���Ͻ�����
    private Camera cam;
    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        cam = GetComponent<Camera>();
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        // ���������Ӧ��λ��
        float targetX = Mathf.Clamp(
            player.position.x,
            mapMin.x + camHalfWidth,
            mapMax.x - camHalfWidth
        );

        float targetY = Mathf.Clamp(
            player.position.y,
            mapMin.y + camHalfHeight,
            mapMax.y - camHalfHeight
        );

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}