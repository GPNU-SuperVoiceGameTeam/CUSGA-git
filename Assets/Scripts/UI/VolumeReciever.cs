using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeReciever : MonoBehaviour
{
    public AudioSource audioSource; 
    public float volume;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        volume = VolumeControlerInstance.Instance.curVolume;
        audioSource.volume = volume;
        
    }
}
