using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class C3BossDead : MonoBehaviour
{
    public GameObject Boss;
    public GameObject transport;
    // Start is called before the first frame update
    void Start()
    {
        transport.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss == null){
            transport.SetActive(true);
        }
    }
}
