using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFightTrigger : MonoBehaviour
{
    public GameObject bossBar;

    public void Awake()
    {
        if (bossBar == null)
        {
            Debug.LogError("Boss Bar not hooked up in game");
        }
    }

    public void OnEnable()
    {
            EventManager.StartListening<PlayerDeathEvent, Vector3>(punchEventListener);
            EventManager.StartListening<DeathAudioEvent, Vector3>(deathEventListener);
            EventManager.StartListening<PlayerHurtAudioEvent, Vector3>(playerHurtEventListener);
            EventManager.StartListening<EnemyHurtAudioEvent, Vector3>(enemyHurtEventListener);
            EventManager.StartListening<CrateHitAudioEvent, Vector3>(crateHitEventListener);
            EventManager.StartListening<MusicAudioEvent, int>(musicEventListener);
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
            bossBar.SetActive(false);
            EventManager.TriggerEvent<MusicAudioEvent, int>(0);
        }
    }

    public void OnPlayerDeath()
    {
        if (bossBar.activeInHierarchy)
        {
            bossBar.SetActive(false);
            EventManager.TriggerEvent<MusicAudioEvent, int>(0);
        }
    }
}
