using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;
    public AudioClip punchAudio;
    public AudioClip deathAudio;
    public AudioClip playerHurtAudio;
    public AudioClip enemyHurtAudio;
    public AudioClip crateHitAudio;

    public AudioSource musicSource;
    public AudioClip baseMusic;
    public AudioClip bossMusic;
    public AudioClip winMusic;

    private UnityAction<Vector3> punchEventListener;
    private UnityAction<Vector3> deathEventListener;
    private UnityAction<Vector3> playerHurtEventListener;
    private UnityAction<Vector3> enemyHurtEventListener;
    private UnityAction<Vector3> crateHitEventListener;
    private UnityAction<int> musicEventListener;

    void Awake()
    {
        punchEventListener = new UnityAction<Vector3>(punchEventHandler);
        deathEventListener = new UnityAction<Vector3>(deathEventHandler);
        playerHurtEventListener = new UnityAction<Vector3>(playerHurtEventHandler);
        enemyHurtEventListener = new UnityAction<Vector3>(enemyHurtEventHandler);
        crateHitEventListener = new UnityAction<Vector3>(crateHitEventHandler);
        musicEventListener = new UnityAction<int>(musicEventHandler);
    }


    // Use this for initialization
    void Start()
    {
        musicSource = GetComponentInChildren<AudioSource>();
    }


    void OnEnable()
    {
        EventManager.StartListening<PunchAudioEvent, Vector3>(punchEventListener);
        EventManager.StartListening<DeathAudioEvent, Vector3>(deathEventListener);
        EventManager.StartListening<PlayerHurtAudioEvent, Vector3>(playerHurtEventListener);
        EventManager.StartListening<EnemyHurtAudioEvent, Vector3>(enemyHurtEventListener);
        EventManager.StartListening<CrateHitAudioEvent, Vector3>(crateHitEventListener);
        EventManager.StartListening<MusicAudioEvent, int>(musicEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<PunchAudioEvent, Vector3>(punchEventListener);
        EventManager.StopListening<DeathAudioEvent, Vector3>(deathEventListener);
        EventManager.StopListening<PlayerHurtAudioEvent, Vector3>(playerHurtEventListener);
        EventManager.StopListening<EnemyHurtAudioEvent, Vector3>(enemyHurtEventListener);
        EventManager.StopListening<CrateHitAudioEvent, Vector3>(crateHitEventListener);
        EventManager.StopListening<MusicAudioEvent, int>(musicEventListener);
    }

    void punchEventHandler(Vector3 worldPos)
    {
        soundEventHandler(worldPos, this.punchAudio);
    }
    void deathEventHandler(Vector3 worldPos)
    {
        soundEventHandler(worldPos, this.deathAudio);
    }
    void playerHurtEventHandler(Vector3 worldPos)
    {
        soundEventHandler(worldPos, this.playerHurtAudio);
    }
    void enemyHurtEventHandler(Vector3 worldPos)
    {
        soundEventHandler(worldPos, this.enemyHurtAudio);
    }
    void crateHitEventHandler(Vector3 worldPos)
    {
        soundEventHandler(worldPos, this.crateHitAudio);
    }


    void soundEventHandler(Vector3 worldPos, AudioClip clip)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {
            //Debug.Log(clip.ToString() + " Sound Played");
            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = clip;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 10f;

            snd.audioSrc.Play();
        }
    }

    void musicEventHandler(int track)
    {
        switch(track)
        {
            case 0:
                playBaseMusic();
                break;
            case 1:
                playBossMusic();
                break;
            case 2:
                playWinMusic();
                break;
            default:
                playBaseMusic();
                break;
        }
    }

    public void playWinMusic()
    {
        musicSource.Stop();
        musicSource.clip = winMusic;
        musicSource.Play();
    }

    public void playBossMusic()
    {
        musicSource.Stop();
        musicSource.clip = bossMusic;
        musicSource.Play();
    }

    public void playBaseMusic()
    {
        musicSource.Stop();
        musicSource.clip = baseMusic;
        musicSource.Play();
    }
}
