using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCUI : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackGame(){
        menu.SetActive(false);
        player.isOpen = false;
        Time.timeScale = 1;
    }
    public void BackMainMenu(){
        SceneManager.LoadScene("Main");
    }
    public void OpenSettings(){
        settings.SetActive(true);
    }
    public void BackESCMenu(){
        settings.SetActive(false);
    }
}
