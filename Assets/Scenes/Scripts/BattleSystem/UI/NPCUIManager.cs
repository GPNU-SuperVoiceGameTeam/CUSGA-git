using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCUIManager : MonoBehaviour
{
    private NPCBattleValueManager nbvm;//获取主人数值管理器
    private Slider hpSlider;//血量条
    
    void Start()
    {
        nbvm = transform.parent.GetComponent<NPCBattleValueManager>();
        Transform HPslider_transform = transform.Find("HPSlider");//注意层级关系
        hpSlider = HPslider_transform.GetComponent<Slider>();
        HpInit();
        
    }

    // Update is called once per frame
    void Update()
    {
        HpUpdate();
        
    }

    void HpInit()
    {
        
        hpSlider.maxValue = nbvm.MaxHP;
        

    }

    void HpUpdate()
    {
        hpSlider.value = nbvm.CurrentHP;

    }
}
