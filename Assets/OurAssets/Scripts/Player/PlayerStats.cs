using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // STATS
    public int health_stat;
    public int max_health;
    public int current_health;

    public int stamina_stat;
    public float max_stamina;
    public float current_stamina;

    public int stamina_regen_enabled = 1;
    public float staminaRegenDelay; //In seconds
    public float stamina_regen_factor;
    private int staminaDelayCount = 0;  //ensures that the stamina has to wait until the last move that used stamina is over

    // UI ELEMENTS
    public HealthBar health_bar;
    public StaminaBar stamina_bar;
    private void Awake()
    {
        if (health_bar == null)
        {
            health_bar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            if (health_bar == null)
            {
                Debug.LogError("Player doesn't have a health bar. Forgot to set reference in inspector?");
            }
        }
        if (stamina_bar == null)
        {
            stamina_bar = GameObject.Find("StaminaBar").GetComponent<StaminaBar>();
            if (stamina_bar == null)
            {
                Debug.LogError("Player doesn't have a stamina bar. Forgot to set reference in inspector?");
            }
        }
    }

    private void Update()
    {
        //Stamina Regeneration
        StaminaCost(-1* stamina_regen_enabled * stamina_regen_factor * Time.timeScale);
        //will not regen on pause
    }

    private void Start()
    {
        max_health = SetMaxHealthFromStat();
        current_health = max_health;

        health_bar.setMaxHealth(max_health);
        health_bar.setCurrentHealth(current_health);

        max_stamina = SetMaxStaminaFromStat();
        current_stamina = max_stamina;

        stamina_bar.setMaxStamina(max_stamina);
        stamina_bar.setCurrentStamina(current_stamina);
    }

    public void SetHealthStat(int _health_stat)
    {
        health_stat = _health_stat;
    }

    private int SetMaxHealthFromStat()
    {
        if (health_stat <= 5)
        {
            max_health = 10 * health_stat;
        } else
        {
            max_health = 50 + 20 * (health_stat - 5);
        }
        return max_health;
    }

    public void SetStaminaStat(int _stamina_stat)
    {
        stamina_stat = _stamina_stat;
    }

    private float SetMaxStaminaFromStat()
    {
        if (stamina_stat <= 5)
        {
            max_stamina = 200 * stamina_stat;
        }
        else
        {
            max_stamina = 1000 + 400 * (stamina_stat - 5);
        }
        return max_stamina;
    }

    // used for damage or for healing when has negative input
    public void TakeDamage(int damage)
    {
        current_health -= damage;
        if (current_health < 0)
        {
            current_health = 0;
        }
        if (current_health > max_health)
        {
            current_health = max_health;
        }

        health_bar.setCurrentHealth(current_health);
    }

    public void DisableStaminaRegen()
    {
        stamina_regen_enabled = 0;
    }

    public void EnableStaminaRegen()
    {
        StartCoroutine(staminaDelay());

    }

    private IEnumerator staminaDelay()
    {
        staminaDelayCount++;
        yield return new WaitForSeconds(staminaRegenDelay);
        staminaDelayCount--;
        if (staminaDelayCount == 0)
        {
            stamina_regen_enabled = 1;
        }
    }

    public void StaminaCost(float cost)
    {
        if (cost > current_stamina)
        {
            cost *= 32; //penalty for overexertion
        }
        current_stamina -= cost;
        if (current_stamina < 0)
        {
            //need to tie to public variables
            stamina_regen_factor = 1;
        }
        if (stamina_regen_factor == 1 && current_stamina >= max_stamina / 2)
        {
            stamina_regen_factor = 2;
        }
        // allows behind-the-scenes negative stamina (soft attack block)
        if (current_stamina > max_stamina)
        {
            current_stamina = max_stamina;
        }

        stamina_bar.setCurrentStamina(current_stamina);
    }
}
