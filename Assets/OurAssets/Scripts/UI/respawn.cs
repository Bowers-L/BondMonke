using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    private GameObject player;
    public HealthBar health_bar;
    private GameObject[] enemies;
    public Material playerMaterial;

    private void Awake()
    {
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
        if (enemies == null)
        {
            Debug.LogError("there are no tagged enemies dangit");
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
                    enemies[i].GetComponent<BasicEnemyAI>().reset = true;
                }
            }
        }
        if (!player.activeSelf)
        {
            player.transform.position = player.GetComponent<PlayerController>().respawnPoint + new Vector3(0, .01f, 0);
            player.GetComponent<PlayerStats>().current_health = player.GetComponent<PlayerStats>().max_health;
            player.GetComponentInChildren<DeathFader>().enabled = false;
            player.GetComponentInChildren<Renderer>().material = playerMaterial;
            player.GetComponent<PlayerController>().enabled = true;
            player.gameObject.SetActive(true);
            health_bar.setCurrentHealth(player.GetComponent<PlayerStats>().current_health);

            player.GetComponent<PlayerController>().player_camera.enabled = true;   //in case the player fell
        }
    }
}
