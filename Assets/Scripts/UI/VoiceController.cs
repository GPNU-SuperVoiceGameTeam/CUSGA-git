using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour
{
    public Image image;
    public float voice;
    public float maxVoice = 100;
    public float decayInterval = 3f; // ˥�����ʱ�䣨�룩
    public float lastAttackTime; // ��һ�ι�����ʱ��
    public bool isDecaying = false; // �Ƿ�����˥��
    public float decayRate = 80;
    public PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        lastAttackTime = Time.time; // ��ʼ��Ϊ��ǰʱ��
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
                isDecaying = true; // ��ʼ˥��
            }
            else
            {
                isDecaying = false; // ����˥��״̬
            }
            if (isDecaying)
            {
                voice -= decayRate * Time.deltaTime; // ÿ֡˥��
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
        lastAttackTime = Time.time; // ����˥����ʱ��
        isDecaying = false; // ����˥��״̬
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
