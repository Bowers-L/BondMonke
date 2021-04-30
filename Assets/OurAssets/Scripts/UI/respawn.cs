using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    private GameObject player;
    public HealthBar health_bar;
    public StaminaBar stamina_bar;
    private GameObject[] enemies;
    private GameObject[] collectables;
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
        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        if (stamina_bar == null)
        {
            stamina_bar = GameObject.Find("StaminaBar").GetComponent<StaminaBar>();
            if (stamina_bar == null)
            {
                Debug.LogError("Player doesn't have a stamina bar. Forgot to set reference in inspector?");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerStats>().current_health <= 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].GetComponentInChildren<DeathFader>().enabled)
                {
                    Material mat = enemies[i].GetComponentInChildren<Renderer>().material;
                    enemies[i].GetComponent<BasicEnemyAI>().enabled = true;
                    enemies[i].GetComponentInChildren<DeathFader>().enabled = false;

                    //Make sure that the enemy respawns in Opaque mode.
                    Utility.SwitchRenderMode(mat, Utility.RenderingModes.Opaque);
                    enemies[i].SetActive(true);
                    enemies[i].GetComponent<BasicEnemyAI>().Respawn();
                } else
                {
                    enemies[i].GetComponent<BasicEnemyAI>().reset = true;
                }

                
            }
        }
        if (!player.activeSelf)
        {
            player.transform.position = player.GetComponent<PlayerController>().respawnPoint + new Vector3(0, .01f, 0);
            player.GetComponent<PlayerStats>().current_health = player.GetComponent<PlayerStats>().max_health;
            player.GetComponent<PlayerStats>().current_stamina = player.GetComponent<PlayerStats>().max_stamina;
            player.GetComponentInChildren<DeathFader>().enabled = false;
            Debug.Log(player.GetComponentInChildren<Renderer>().material);
            player.GetComponentInChildren<Renderer>().material = playerMaterial;
            
            player.GetComponent<PlayerController>().enabled = true;
            player.gameObject.SetActive(true);
            health_bar.setCurrentHealth(player.GetComponent<PlayerStats>().current_health);
            stamina_bar.setCurrentStamina(player.GetComponent<PlayerStats>().current_stamina);
            player.GetComponent<PlayerStats>().stamina_regen_enabled = 1;
            //player.GetComponent<PlayerStats>().staminaDelayCount = 0;
            player.GetComponent<PlayerController>().player_camera.enabled = true;   //in case the player fell
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;   //make sure the player doesn't fly off
            player.GetComponent<BallCollector>().ResetCollectableCount();
            //player.GetComponent<Rigidbody>().isKinematic = true;

            foreach (GameObject collectable in collectables)
            {
                collectable.SetActive(true);
            }
        }
    }
}
