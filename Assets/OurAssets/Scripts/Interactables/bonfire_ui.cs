using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class bonfire_ui : MonoBehaviour
{
    public GameObject manager;
    private GameObject player;
    private CanvasGroup canvasGroup;
    public HealthBar health_bar;
    public StaminaBar stamina_bar;
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
        if (enemies == null)
        {
            Debug.LogError("there are no tagged enemies dangit");
        }
        if (stamina_bar == null)
        {
            stamina_bar = GameObject.Find("StaminaBar").GetComponent<StaminaBar>();
            if (stamina_bar == null)
            {
                Debug.LogError("Player doesn't have a stamina bar. Forgot to set reference in inspector?");
            }
        }

        GameManager.Instance.controls.UI.Interact.performed += ctx => OnPlayerRest();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerRest()
    {
        if (player.GetComponent<PlayerController>().enteredBonfire)
        {
            if (canvasGroup.interactable)
            {
                manager.GetComponent<manager>().switcher();
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                player.GetComponent<PlayerInputController>().enabled = true;
                Time.timeScale = 1f;
            }
            else
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].activeSelf)
                    {
                        enemies[i].GetComponent<BasicEnemyAI>().transform.position = enemies[i].GetComponent<BasicEnemyAI>().originPoint;
                        enemies[i].GetComponent<BasicEnemyAI>().reset = true;
                    }
                }
                player.GetComponent<PlayerController>().respawnPoint = player.transform.position;
                health_bar.setCurrentHealth(player.GetComponent<PlayerStats>().max_health);
                stamina_bar.setCurrentStamina(player.GetComponent<PlayerStats>().max_health);
                player.GetComponent<PlayerStats>().current_health = player.GetComponent<PlayerStats>().max_health;
                player.GetComponent<PlayerStats>().current_stamina = player.GetComponent<PlayerStats>().max_stamina;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                player.GetComponent<PlayerInputController>().enabled = false;
                Time.timeScale = 0f;
            }
        }

        GameObject.FindObjectOfType<bonfirePrompt>().TogglePrompt();
    }
}
