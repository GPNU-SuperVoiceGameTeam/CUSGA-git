using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    bool isEnter = false;
    void Start()
    {
        audioSource.volume = 0;
        audioSource.Stop();
    }
    void Update()
    {
        if(isEnter){
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1, Time.deltaTime * 0.5f);
        }else{
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0, Time.deltaTime * 0.5f);
            if(audioSource.volume <= 0.1f){
                audioSource.Stop();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        isEnter = true;
        audioSource.Play();
    }
    void OnTriggerStay2D(Collider2D collision){
        
    }
    void OnTriggerExit2D(Collider2D collision){
        
        isEnter = false;
    }
}
