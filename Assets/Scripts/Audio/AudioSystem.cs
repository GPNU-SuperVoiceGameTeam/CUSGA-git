using System;
using System.Collections.Generic;
using UnityEngine;
 
public enum AudioType
{
    BGM,
    Click,
    Building,
    Success,
    Error,
    ResourceGet,
    DayFinish,
    GameWin,
    GameOver
}
public class AudioManager : MonoBehaviour
{
    [Header("音频数据")]
    public List<AudioData> audioData;
 
    private Dictionary<AudioType,AudioData> _audioDataDic;
    
    [Serializable]
    public struct AudioData
    {
        public AudioType type;
        public AudioClip audioClip;
        public AudioSource audioSource;
    }
 
    private static AudioManager _instance;
    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        
        IniAudioSource();
    }
 
    private void OnEnable()
    {
        PlayBackgroundMusic();
        AudioEventController.OnPlayAudio += PlayerAudio;
    }
 
    private void OnDisable()
    {
        AudioEventController.OnPlayAudio -= PlayerAudio;
    }
 
    private void IniAudioSource()
    {
        _audioDataDic=new Dictionary<AudioType, AudioData>();
        
        foreach (var audio in audioData)
        {
            if (audio.audioSource != null)
            {
                audio.audioSource.clip = audio.audioClip;
            }
            _audioDataDic[audio.type] = audio;
        }
    }
    private void PlayerAudio(AudioType audioType)
    {
        if (_audioDataDic.TryGetValue(audioType, out AudioData audioData))
        {
            _audioDataDic[audioType].audioSource.Play();
            Debug.Log(audioType);
        }
            
    }
    private void PlayBackgroundMusic()
{
    if (_audioDataDic.TryGetValue(AudioType.BGM, out var data))
    {
        data.audioSource.Play();
    }
}
}