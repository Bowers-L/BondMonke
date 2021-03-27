using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class respawn : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private GameObject player;
    public HealthBar health_bar;
    private GameObject[] enemies;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("The component CanvasGroup is missing");
        }
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player is not found");
        }
        if (health_bar == null)
        {
            health_bar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            if (health_bar == null)
            {
                Debug.LogError("Player doesn't have a health bar. Forgot to set reference in inspector?");
            }
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies[0] == null)
        {
            Debug.LogError("there are no tagged enemies fuck");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerStats>().current_health <= 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].activeSelf)
                {
                    enemies[i].GetComponent<BasicEnemyAI>().transform.position = enemies[i].GetComponent<BasicEnemyAI>().originPoint;
                    enemies[i].GetComponent<BasicEnemyAI>().reset = true;
                }
            }
            player.gameObject.SetActive(false);
            player.transform.position = player.GetComponent<PlayerController>().respawnPoint;
            player.GetComponent<PlayerStats>().current_health = player.GetComponent<PlayerStats>().max_health;
            player.gameObject.SetActive(true);
            health_bar.setCurrentHealth(player.GetComponent<PlayerStats>().current_health);
        }
    }

    private void Respawn()
    {

    }
}
