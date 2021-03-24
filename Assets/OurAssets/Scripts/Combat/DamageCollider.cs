using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;

    //for now, each collider will deal a certain amount of damage, and we can have a different collider per attack.
    //Alternatively, we could set the damageAmount every time the collider is enabled to reuse colliders.
    public int damageAmount;   

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void EnableDamageCollider(int damageAmount)
    {
        EnableDamageCollider();
        this.damageAmount = damageAmount;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Hittable"))
        {
            //collision

            //idk if we should trigger an event for this since it should only affect what is hit.
            //EventManager.TriggerEvent<DamageEvent, int>(5);
            CombatAgent opponent = other.GetComponent<CombatAgent>();
            opponent.TakeDamage();
        }
    }
}
