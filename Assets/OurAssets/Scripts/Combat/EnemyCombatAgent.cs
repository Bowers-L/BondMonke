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
    public override void GetHit(GameObject opponent, AttackInfo attack)
    {
        if (lastUsedCollider != null)
        {
            lastUsedCollider.DisableDamageCollider();
        }
        
        if (isBlocking)
        {
            if (attack.breaksGuard)
            {
                GetComponent<Animator>().SetTrigger("BlockBroken");
            }
        }
        else if (!isInvincible)
        {
            EnemyStats stats = GetComponent<EnemyStats>();
            if (stats != null && attack != null && gameObject.activeInHierarchy)
            {
                stats.TakeDamage(attack.damage);
                if (stats.current_health > 0)
                {
                    GetComponent<Animator>().SetTrigger("HitFrom" + attack.attackName);
                }
            }
        }
    }
}
