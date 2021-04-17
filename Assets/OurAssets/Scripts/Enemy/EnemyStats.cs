using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // STATS
    public int health_stat;
    public int max_health;
    public int current_health;

    // UI ELEMENTS
    //public HealthBar health_bar;

    private void Awake()
    {
        /*
        if (health_bar == null)
        {
            health_bar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            if (health_bar == null)
            {
                Debug.LogError("Enemy doesn't have a health bar. Forgot to set reference in inspector?");
            }
        }
        */
    }

    private void Start()
    {
        max_health = SetMaxHealthFromStat();
        current_health = max_health;

        //health_bar.setMaxHealth(max_health);
        //health_bar.setCurrentHealth(current_health);
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
        }
        else
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

        //health_bar.setCurrentHealth(current_health);
    }

    //Need these so that the animation event has a target, but it's better if
    //we can figure out how to not have these without creating entirely separate
    //animations for the enemy
    public void DisableStaminaRegen()
    {

    }

    public void EnableStaminaRegen()
    {

    }
}
