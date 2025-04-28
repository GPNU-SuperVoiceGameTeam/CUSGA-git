using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class C3BossTrigger : MonoBehaviour
{
    public GameObject boss_Show;
    public GameObject boss;
    public PlayableDirector bossTimeline;
    // Start is called before the first frame update
    private void Start()
    {
        if (bossTimeline != null)
        {
            bossTimeline.stopped += OnTimelineStopped;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            bossTimeline.Play();
        }
    }
    private void OnTimelineStopped(PlayableDirector director)
    {
        boss.SetActive(true);
        boss_Show.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (bossTimeline != null)
        {
            bossTimeline.stopped -= OnTimelineStopped;
        }
    }
}
