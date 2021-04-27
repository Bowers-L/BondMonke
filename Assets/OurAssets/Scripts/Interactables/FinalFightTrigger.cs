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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossBar.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bossBar.SetActive(false);
    }
}
