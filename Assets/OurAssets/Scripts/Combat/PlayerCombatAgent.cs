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
    public override void GetHit(GameObject opponent, AttackInfo attack)
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        if (lastUsedCollider != null)
        {
            lastUsedCollider.DisableDamageCollider();
        }

        if (isBlocking)
        {
            
            stats.StaminaCost(attack.staminaPenaltyOnGuard);
            if (stats.current_stamina <= 0)
            {
                GetComponent<Animator>().SetTrigger("BlockBroken");
            }
            
        } else if (!isInvincible)  //Gives the player i-frames so that the combat is more fair.
        {
            GetComponent<PlayerStats>().TakeDamage(attack.damage);
            if (stats.current_health > 0)
            {
                if (stats.current_poise <= 0)
                {
                    GetComponent<Animator>().SetTrigger("HitFrom" + attack.attackName);
                    stats.ResetPoise();
                }
                EventManager.TriggerEvent<PlayerHurtAudioEvent, Vector3>(opponent.transform.position);
            }
            
            if (attack.force > 0)
            {
                //This is really hacky if the attack isn't performed in front of the enemy, 
                //but all of our attacks are and we're only doing this with one enemy.
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.AddForce(attack.force * opponent.transform.forward, ForceMode.Impulse);
                //rb.AddForceAtPosition(attack.force * opponent.transform.forward, opponent.GetComponentInChildren<DamageCollider>().transform.position, ForceMode.Impulse);
            }
        }

    }
}
