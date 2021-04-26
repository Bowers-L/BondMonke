using UnityEngine;

class PlayerCombatAgent : CombatAgent
{
    public override void GetHit(AttackInfo attack)
    {
        GetComponent<PlayerStats>().TakeDamage(attack.damage);
        GetComponent<Animator>().SetTrigger("HitFrom" + attack.attackName);
    }
}
