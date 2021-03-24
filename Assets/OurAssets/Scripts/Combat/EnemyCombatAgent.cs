using UnityEngine;

class EnemyCombatAgent : CombatAgent
{
    public override void TakeDamage(int damage)
    {
        GetComponent<EnemyStats>().TakeDamage(damage);
    }
}
