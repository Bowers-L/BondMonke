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
        PlayerStats stats = GetComponent<PlayerStats>();
        if (isBlocking)
        {
            
            stats.StaminaCost(attack.staminaPenaltyOnGuard);
            if (stats.current_stamina <= 0)
            {
                GetComponent<Animator>().SetTrigger("BlockBroken");
            }
            
        } else if (!isInvincible)  //Gives the player i-frames so that the combat is more fair.
        {
            lastUsedCollider.DisableDamageCollider();   
            GetComponent<PlayerStats>().TakeDamage(attack.damage);
            if (stats.current_health > 0)
            {
                GetComponent<Animator>().SetTrigger("HitFrom" + attack.attackName);
            }
            
        }

    }
}
