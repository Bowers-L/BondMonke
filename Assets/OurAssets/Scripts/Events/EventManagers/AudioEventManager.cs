using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;
    public AudioClip punchAudio;
    public AudioClip deathAudio;

    private UnityAction<Vector3> punchEventListener;
    private UnityAction<Vector3> deathEventListener;

    void Awake()
    {
        punchEventListener = new UnityAction<Vector3>(punchEventHandler);
        deathEventListener = new UnityAction<Vector3>(deathEventHandler);
    }


    // Use this for initialization
    void Start()
    {


        			
    }


    void OnEnable()
    {
        EventManager.StartListening<PunchAudioEvent, Vector3>(punchEventListener);
        EventManager.StartListening<DeathAudioEvent, Vector3>(deathEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<PunchAudioEvent, Vector3>(punchEventListener);
        EventManager.StartListening<DeathAudioEvent, Vector3>(deathEventListener);
    }

    void punchEventHandler(Vector3 worldPos)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {
            Debug.Log("Punch Sound Played");
            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.punchAudio;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }

    void deathEventHandler(Vector3 worldPos)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {
            Debug.Log("Death Sound Played");
            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.deathAudio;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }

}
