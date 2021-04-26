using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class makes it easier for combatants to interact via hit/hurt boxes.

public abstract class CombatAgent : MonoBehaviour
{

    public bool isBlocking;
    public bool isInvincible;

    protected DamageCollider lastUsedCollider;

    protected void Awake()
    {
        isBlocking = false;
        isInvincible = false;
    }

    public void EnableHitbox(DamageCollider hitbox)
    {
        hitbox.EnableDamageCollider();
        lastUsedCollider = hitbox;
    }

    //Call AttackWithDamage during an attack to add a hitbox/damage value to the player.
    public void EnableHitboxWithAttack(DamageCollider hitbox, AttackInfo attack)
    {
        //enables the hitbox and sets the amount of damage the attack should do.
        hitbox.attack = attack;
        EnableHitbox(hitbox);
    }

    //Call FinishAttack after the attack animation finishes to remove the hitbox.
    public void DisableHitbox()
    {
        lastUsedCollider.DisableDamageCollider();
    }

    public void SetHitboxDamage(DamageCollider hitbox, AttackInfo attack)
    {
        hitbox.attack = attack;
    }

    //Call TakeDamage everytime a hitbox collides with an opposing hurtbox.
    //This could be different based on if it's a player or enemy.
    public abstract void GetHit(GameObject opponent, AttackInfo attack);
}
