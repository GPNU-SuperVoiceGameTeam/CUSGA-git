using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDistanceControl : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform playerTransform; // 玩家或其他目标的Transform
    public float maxVolume = 1f;
    public float minVolume = 0f;
    public float maxDistance = 10f;

    void Update()
    {
        // 计算当前距离
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        
        // 根据距离计算音量
        float volume = Mathf.Clamp(1 - (distance / maxDistance), minVolume, maxVolume);

        // 设置音量
        audioSource.volume = volume;
    }
}
