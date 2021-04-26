using UnityEngine;

class EnemyCombatAgent : CombatAgent
{
    BasicEnemyAI ai;

    void Awake()
    {
        base.Awake();
        ai = GetComponent<BasicEnemyAI>();
        if (ai == null)
        {
            Debug.LogError("Enemy has no AI Component");
        }
    }
    public override void GetHit(AttackInfo attack)
    {
        GetComponent<EnemyStats>().TakeDamage(attack.damage);
        GetComponent<Animator>().SetTrigger("HitFrom" + attack.attackName);
    }
}
