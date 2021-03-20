using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // STATS
    public int health_stat;
    public int max_health;
    public int current_health;

    // UI ELEMENTS
    public HealthBar health_bar;

    private void Start()
    {
        max_health = SetMaxHealthFromStat();
        current_health = max_health;

        health_bar.setMaxHealth(max_health);
        health_bar.setCurrentHealth(current_health);
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
}
