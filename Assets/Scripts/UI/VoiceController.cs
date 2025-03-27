using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour
{
    public Image image;
    public float voice;
    public float maxVoice = 100;
    public float decayInterval = 3f; // 衰减间隔时间（秒）
    public float lastAttackTime; // 上一次攻击的时间
    public bool isDecaying = false; // 是否正在衰减
    public float decayRate = 80;
    public PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        lastAttackTime = Time.time; // 初始化为当前时间
    }
    void Update()
    {
        changeVoice();
        if(voice >= 100)
        {
            playerController.canShoot = false;
        }
        else
        {
            playerController.canShoot = true;
        }
        if (Time.time - lastAttackTime > decayInterval)
        {
            if (!isDecaying)
            {
                isDecaying = true; // 开始衰减
            }
            else
            {
                isDecaying = false; // 重置衰减状态
            }
            if (isDecaying)
            {
                voice -= decayRate * Time.deltaTime; // 每帧衰减
                if (voice < 0)
                {
                    voice = 0;
                }
            }
        }
    }

    public void changeVoice()
    {
        image.fillAmount = Mathf.Lerp(image.fillAmount, voice / maxVoice, Time.deltaTime * 5);
    }
    public void AddVoice()
    {
        voice += 10;
        if (voice > maxVoice)
        {
            voice = maxVoice;
        }
        lastAttackTime = Time.time; // 重置衰减计时器
        isDecaying = false; // 重置衰减状态
    }

    public void ReduceVoice()
    {
        voice -= 10;
        if (voice < 0)
        {
            voice = 0;
        }
    }
}
