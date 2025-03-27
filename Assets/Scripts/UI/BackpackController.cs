using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackController : MonoBehaviour
{
    public Sprite[] specialWave_img;
    public int selectImg = 0;
    public WheelController wheelController;
    void Start()
    {
        this.GetComponent<Image>().sprite = specialWave_img[0];
    }

    // Update is called once per frame
    void Update()
    {
        selectImg = wheelController.selectedOption;
        this.GetComponent<Image>().sprite = specialWave_img[(int)selectImg];
    }
}
