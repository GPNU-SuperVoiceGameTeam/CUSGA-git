using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour
{
    public Image image;
    public float voice;
    public float maxVoice = 100;
    public float decayInterval = 3f; //衰减间隔
    public float lastAttackTime; //上次攻击时间
    public bool isDecaying = false;
    public float decayRate = 80; //衰减速率
    public PlayerController playerController;

    private void Start()
    {
        lastAttackTime = Time.time;
    }
    void Update()
    {
        changeVoice();
        if(voice >= 100)
        {
            playerController.canAttack = false; 
        }
        else
        {
            playerController.canAttack = true;
        }
        if (Time.time - lastAttackTime > decayInterval)
        {
            if (!isDecaying)
            {
                isDecaying = true; 
            }
            else
            {
                isDecaying = false; 
            }
            if (isDecaying)
            {
                voice -= decayRate * Time.deltaTime;
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
        lastAttackTime = Time.time; 
        isDecaying = false; 
    }
}
