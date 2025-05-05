using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCUI : MonoBehaviour
{
    public GameObject menu;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void BackGame(){
        AudioEventController.RaiseOnPlayAudio(AudioType.Click);
        menu.SetActive(false);
        player.isOpen = false;
        Time.timeScale = 1;
    }
    public void BackMainMenu(){
        AudioEventController.RaiseOnPlayAudio(AudioType.Click);
        SceneManager.LoadScene("Main");
    }
}
