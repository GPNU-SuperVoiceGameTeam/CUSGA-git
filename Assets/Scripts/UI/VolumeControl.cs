using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 需要引用 UI 命名空间

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider; 
    public AudioSource audioSource; 

    void Start()
    {
        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        audioSource = GameObject.Find("music").GetComponent<AudioSource>();

        volumeSlider.value = audioSource.volume;

        // 绑定滑动条值变化事件
        volumeSlider.onValueChanged.AddListener(HandleVolumeChanged);
    }

    void HandleVolumeChanged(float volume)
    {
        // 设置音量（0~1）
        audioSource.volume = volume;

        // 可选：保存音量设置（如 PlayerPrefs）
        // PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    void OnDestroy()
    {
        // 移除事件监听（避免内存泄漏）
        volumeSlider.onValueChanged.RemoveListener(HandleVolumeChanged);
    }
}
