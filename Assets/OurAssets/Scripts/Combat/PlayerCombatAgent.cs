using UnityEngine;

class PlayerCombatAgent : CombatAgent
{
    public override void TakeDamage(int damage)
    {
        GetComponent<PlayerStats>().TakeDamage(damage);
    }
}
