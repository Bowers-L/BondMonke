using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class makes it easier for combatants to interact via hit/hurt boxes.

public abstract class CombatAgent : MonoBehaviour
{
    private DamageCollider lastUsedCollider;

    public void StartAttack(DamageCollider hitbox)
    {
        hitbox.EnableDamageCollider();
        lastUsedCollider = hitbox;
    }

    //Call AttackWithDamage during an attack to add a hitbox/damage value to the player.
    public void StartAttackWithDamage(DamageCollider hitbox, int damageAmount)
    {
        //enables the hitbox and sets the amount of damage the attack should do.
        hitbox.damageAmount = damageAmount;
        StartAttack(hitbox);
    }

    //Call FinishAttack after the attack animation finishes to remove the hitbox.
    public void FinishAttack()
    {
        lastUsedCollider.DisableDamageCollider();
    }

    public void SetDamage(DamageCollider hitbox, int damageAmount)
    {
        hitbox.damageAmount = damageAmount;
    }

    //Call TakeDamage everytime a hitbox collides with an opposing hurtbox.
    //This could be different based on if it's a player or enemy.
    public abstract void TakeDamage(int damage);
}
