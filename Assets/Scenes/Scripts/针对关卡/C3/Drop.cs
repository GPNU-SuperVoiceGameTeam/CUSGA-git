using System.Collections;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips; // 音频数组
    private AudioSource audioSource; // 用于播放音频的 AudioSource

    void Start()
    {
        // 初始化 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = 0.5f; // 设置音量为 0.5

        // 开始循环逻辑
        StartCoroutine(PlayRandomAudioLoop());
    }

    IEnumerator PlayRandomAudioLoop()
    {
        while (true) // 无限循环
        {
            // 生成随机数 a（单位：秒）
            float randomDelayA = Random.Range(3f, 7f);
            yield return new WaitForSeconds(randomDelayA);

            // 生成随机数 b（作为音频数组的索引）
            int randomIndexB = Random.Range(0, audioClips.Length);

            // 播放下标为 b 的音频
            if (audioClips[randomIndexB] != null)
            {
                audioSource.clip = audioClips[randomIndexB];
                audioSource.Play();
            }

            // 再次生成随机数 a，继续循环
            yield return new WaitForSeconds(2);
        }
    }
}