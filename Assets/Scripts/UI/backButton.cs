using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backButton : MonoBehaviour
{
    public GameObject backpack;
    public PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void back(){
        backpack.SetActive(!backpack.activeSelf);

            // 根据背包的显示状态设置 canAttack 和 canMove
            if (backpack.activeSelf)
            {
                Time.timeScale = 0;
                playerController.canAttack = false;
                playerController.canMove = false;
            }
            else
            {
                Time.timeScale = 1;
                playerController.canAttack = true;
                playerController.canMove = true;
            }
    }
}
