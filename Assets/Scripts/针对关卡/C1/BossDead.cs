using UnityEngine;
using UnityEngine.Playables;

public class BossDead : MonoBehaviour
{
    public GameObject boss;
    public PlayableDirector bossTimeline;
    public GameObject jumpWave;
    private void Start()
    {
        if (bossTimeline != null)
        {
            bossTimeline.played += OnTimelinePlayed;
            bossTimeline.stopped += OnTimelineStopped;
        }
    }
    void Update()
    {
        if(boss == null){
            bossTimeline.Play();
        }
    }
    private void OnTimelinePlayed(PlayableDirector director)
    {
        // Optional: You can add some logic here when the timeline starts playing.
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        jumpWave.SetActive(true);
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
