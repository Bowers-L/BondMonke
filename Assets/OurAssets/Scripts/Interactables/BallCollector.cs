using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollector : MonoBehaviour
{
    public int collectableCount = 0;
    public int healthIncrease;
    public bool final = false;
    public GameObject canvas;

    public CollectableUI ui;

    public void Awake()
    {
        ui = GameObject.Find("GameOverlayUI").GetComponentInChildren<CollectableUI>();
    }

    public void ReceiveBall()
    {
        collectableCount++;
        ui.SetCollectableScore(collectableCount);

        //Increase player health?
        PlayerStats stats = GetComponent<PlayerStats>();
        if (stats)
        {
            stats.TakeDamage(-healthIncrease);
        } else
        {
            Debug.LogWarning("No Stats Component on BallCollector");
        }
        
    }
    public void ReceiveFinal()
    {
        final = true;
    }
}
