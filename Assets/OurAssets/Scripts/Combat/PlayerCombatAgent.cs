using UnityEngine;

class PlayerCombatAgent : CombatAgent
{
    PlayerController controller;

    void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();
        if (controller == null)
        {
            Debug.LogError("Player has no controller");
        }
    }
    public override void GetHit(AttackInfo attack)
    {
        if (isBlocking)
        {
            PlayerStats stats = GetComponent<PlayerStats>();
            GetComponent<PlayerStats>().StaminaCost(attack.staminaPenaltyOnGuard);
            if (stats.current_stamina <= 0)
            {
                GetComponent<Animator>().SetTrigger("BlockBroken");
            }
            
        } else if (!isInvincible)  //Gives the player i-frames so that the combat is more fair.
        {
            GetComponent<PlayerStats>().TakeDamage(attack.damage);
            GetComponent<Animator>().SetTrigger("HitFrom" + attack.attackName);
        }

    }
}
