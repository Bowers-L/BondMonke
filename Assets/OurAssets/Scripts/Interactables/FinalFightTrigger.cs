using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalFightTrigger : MonoBehaviour
{
    public GameObject bossBar;

    private UnityAction<Vector3> playerDeathListener;

    public void Awake()
    {
        if (bossBar == null)
        {
            Debug.LogError("Boss Bar not hooked up in game");
        }

        playerDeathListener = new UnityAction<Vector3>(OnPlayerDeath);
    }

    public void OnEnable()
    {
        EventManager.StartListening<PlayerDeathEvent, Vector3>(playerDeathListener);
    }

    public void OnDisable()
    {
        EventManager.StopListening<PlayerDeathEvent, Vector3>(playerDeathListener);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossBar.SetActive(true);
            EventManager.TriggerEvent<MusicAudioEvent, int>(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossBar.GetComponentInChildren<BossBar>().resetToMax();
            bossBar.SetActive(false);
            EventManager.TriggerEvent<MusicAudioEvent, int>(0);
        }
    }

    public void OnPlayerDeath(Vector3 playerPos)
    {
        if (bossBar.activeInHierarchy)
        {
            bossBar.GetComponentInChildren<BossBar>().resetToMax();
            bossBar.SetActive(false);
            EventManager.TriggerEvent<MusicAudioEvent, int>(0);
        }
    }
}
