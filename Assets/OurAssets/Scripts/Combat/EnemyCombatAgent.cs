using UnityEngine;

class EnemyCombatAgent : CombatAgent
{
    public override void GetHit(AttackInfo attack)
    {
        GetComponent<EnemyStats>().TakeDamage(attack.damage);
    }
}
