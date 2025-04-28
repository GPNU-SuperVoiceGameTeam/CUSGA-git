using System.Collections;
using Fungus;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    public GameObject originalMusic; // 原音乐对象
    public GameObject newMusic;     // 新音乐对象

    private AudioSource originalAudioSource;
    private AudioSource newAudioSource;

    private bool isSwitching = false; // 标记是否正在切换音乐

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
                if(newAudioSource.volume < 1){
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
}
