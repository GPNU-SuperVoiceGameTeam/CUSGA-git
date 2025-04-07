using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawnController : MonoBehaviour
{
    public SpikeController spikeController;
    public GameObject spike;
    void Start()
    {
        spikeController = spike.GetComponent<SpikeController>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            spikeController.spawnPoint = gameObject;
        }
    }
}
