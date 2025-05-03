using System.Collections;
using Fungus;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    public GameObject originalMusic; // 原音乐对象
    public GameObject newMusic;     // 新音乐对象

    private AudioSource originalAudioSource;
    private AudioSource newAudioSource;
    public float musicVolume;

    public bool isSwitching = false; // 标记是否正在切换音乐

    private void Start()
    {
        originalAudioSource = originalMusic.GetComponent<AudioSource>();
        newAudioSource = newMusic.GetComponent<AudioSource>();
        
    }
    void Update()
    {
        if(isSwitching){
            originalAudioSource.volume -= Time.deltaTime * 0.5f;
            if(originalAudioSource.volume <= 0){
                originalMusic.SetActive(false);
                newMusic.SetActive(true);
                if(newAudioSource.volume < musicVolume){
                    newAudioSource.volume += Time.deltaTime * 1f;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            isSwitching = true;
        }
    }
    public void SwitchBackToOriginalMusic()
    {
        isSwitching = false; // 确保不会同时进行两个方向的切换
        newAudioSource.volume -= Time.deltaTime * 0.5f;
            if(newAudioSource.volume <= 0){
                newMusic.SetActive(false);
                originalMusic.SetActive(true);
                if(originalAudioSource.volume < musicVolume){
                    originalAudioSource.volume += Time.deltaTime * 1f;
                }
            }
    }
}
