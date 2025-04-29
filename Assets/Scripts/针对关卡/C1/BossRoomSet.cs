using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossRoomSet : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject obstacle;
    void OnTriggerEnter2D(Collider2D collision)
    {
            director.Play();
            obstacle.SetActive(true);
            this.gameObject.SetActive(false);
    }
}
