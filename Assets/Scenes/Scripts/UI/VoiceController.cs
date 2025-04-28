using UnityEngine;
using UnityEngine.UI;

public class VoiceController : MonoBehaviour
{
    public Image image;
    public float voice;
    public float maxVoice = 100;
    public bool isDecaying;
    public float decayRate = 80; //衰减速率
    public GameObject rotateRecord;
    public PlayerController playerController;

    void Update()
    {
        RotateRecord();
        changeVoice();
        if(voice >= maxVoice || isDecaying){
            playerController.canAttack = false;
            VoiceDecay();
        }
        else
        {
            playerController.canAttack = true;
        }
        
    }

    public void changeVoice()
    {
        image.fillAmount = Mathf.Lerp(image.fillAmount, voice / maxVoice, Time.deltaTime * 5);
    }
    public void AddVoice(int value)
    {
        voice += value;
        if (voice > maxVoice)
        {
            voice = maxVoice;
        }
        isDecaying = false; 
    }
    public void VoiceDecay(){
        isDecaying = true;
        if(isDecaying)
            voice -= decayRate * Time.deltaTime;
        if (voice <= 0)
        {
            isDecaying = false;
            voice = 0;
            playerController.canAttack = true;
        }
    }
    public void RotateRecord(){
        rotateRecord.transform.Rotate(0, 0, 1);
    }
}
