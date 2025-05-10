using UnityEngine;
using UnityEngine.Playables;

public class BossBegin : MonoBehaviour
{
    public GameObject boss;
    public GameObject boss_Show;
    public PlayableDirector bossTimeline;
    public MusicChange musicChange;
    public GameObject mainCamera;
    public GameObject player;
    public GameObject playerCamera;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.SetActive(false);
        musicChange = GameObject.FindGameObjectWithTag("MusicChange").GetComponent<MusicChange>();
        if (bossTimeline != null)
        {
            bossTimeline.played += OnTimelinePlayed;
            bossTimeline.stopped += OnTimelineStopped;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCamera.SetActive(false);
        Vector2 playerPos = player.transform.position;
        mainCamera.transform.position = playerPos;
        mainCamera.SetActive(true);
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
        mainCamera.SetActive(false);
        playerCamera.SetActive(true);
        Destroy(mainCamera);
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
