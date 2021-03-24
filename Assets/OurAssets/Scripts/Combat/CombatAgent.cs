using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatAgent : MonoBehaviour
{

    public void AttackWithDamage(DamageCollider hitbox, int damageAmount)
    {
        hitbox.EnableDamageCollider(damageAmount);
    }

    public abstract void TakeDamage(int damage);
}
