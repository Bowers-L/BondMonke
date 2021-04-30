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

        Animator thisAnim = GetComponent<Animator>();
        EnemyStats stats = GetComponent<EnemyStats>();

        if (isBlocking)
        {
            if (attack.breaksGuard)
            {
                thisAnim.SetTrigger("BlockBroken");
            }
        }
        else if (thisAnim.GetCurrentAnimatorStateInfo(thisAnim.GetLayerIndex("Combat")).IsTag("EnemyStagger"))
        {
            EventManager.TriggerEvent<EnemyHurtAudioEvent, Vector3>(opponent.transform.position);
            stats.TakeDamage(attack.damage);
        }
        else if (!isInvincible)
        {
            
            if (stats != null && attack != null && gameObject.activeInHierarchy)
            {
                stats.TakeDamage(attack.damage);
                if (stats.current_health > 0)
                {
                    EventManager.TriggerEvent<EnemyHurtAudioEvent, Vector3>(opponent.transform.position);
                    GetComponent<Animator>().SetTrigger("HitFrom" + attack.attackName);
                }
            }
        }


    }
}
