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
            GetComponent<PlayerStats>().StaminaCost(attack.staminaPenaltyOnGuard);
        } else if (!isInvincible)  //Gives the player i-frames so that the combat is more fair.
        {
            GetComponent<PlayerStats>().TakeDamage(attack.damage);
            GetComponent<Animator>().SetTrigger("HitFrom" + attack.attackName);
        }

    }
}
