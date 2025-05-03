using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2BossDead : MonoBehaviour
{
    public GameObject Boss;
    public GameObject portal;
    public MusicChange musicChange;
    // Start is called before the first frame update
    void Start()
    {
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss == null){
            portal.SetActive(true);
            musicChange.SwitchBackToOriginalMusic();
        }
    }
}
