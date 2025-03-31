using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour
{
    public Image image;
    public float voice;
    public float maxVoice = 100;
    public float decayInterval = 3f; // �0�9�1�7�1�7�1�7�1�7�1�7�0�2�1�7�4�4�1�7�0�0
    public float lastAttackTime; // �1�7�1�7�0�5�1�7�Ʉ1�7�1�7�1�7�1�7�1�7�0�2�1�7�1�7
    public bool isDecaying = false; // �1�7�0�9�1�7�1�7�1�7�1�7�1�7�0�9�1�7�1�7
    public float decayRate = 80;
    public PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        lastAttackTime = Time.time; // �1�7�1�7�0�3�1�7�1�7�0�2�1�7�1�7�0�2�0�2�1�7�1�7
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
                isDecaying = true; // �1�7�1�7�0�3�0�9�1�7�1�7
            }
            else
            {
                isDecaying = false; // �1�7�1�7�1�7�1�7�0�9�1�7�1�7�0�8�0�0
            }
            if (isDecaying)
            {
                voice -= decayRate * Time.deltaTime; // �0�7�0�5�0�9�1�7�1�7
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
        lastAttackTime = Time.time; // �1�7�1�7�1�7�1�7�0�9�1�7�1�7�1�7�1�7�0�2�1�7�1�7
        isDecaying = false; // �1�7�1�7�1�7�1�7�0�9�1�7�1�7�0�8�0�0
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
