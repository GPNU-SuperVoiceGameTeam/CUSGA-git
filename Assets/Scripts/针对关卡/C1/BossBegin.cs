using UnityEngine;
using UnityEngine.Playables;

public class BossBegin : MonoBehaviour
{
    public GameObject boss;
    public GameObject boss_Show;
    public PlayableDirector bossTimeline;
    public MusicChange musicChange;
    private void Start()
    {
        if (bossTimeline != null)
        {
            bossTimeline.played += OnTimelinePlayed;
            bossTimeline.stopped += OnTimelineStopped;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bossTimeline.Play();
        musicChange.isSwitching = true;
    }

    private void OnTimelinePlayed(PlayableDirector director)
    {
        // Optional: You can add some logic here when the timeline starts playing.
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
            bossTimeline.played -= OnTimelinePlayed;
            bossTimeline.stopped -= OnTimelineStopped;
        }
    }
}
