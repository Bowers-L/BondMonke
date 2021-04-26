﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;

    //for now, each collider will deal a certain amount of damage, and we can have a different collider per attack.
    //Alternatively, we could set the damageAmount every time the collider is enabled to reuse colliders.
    public AttackInfo attack;
    public float torqueFactor = 10f;

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

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Hittable"))
        {
            Debug.Log("Damage Collision");

            //idk if we should trigger an event for this since it should only affect what is hit.
            //EventManager.TriggerEvent<DamageEvent, int>(5);
            CombatAgent opponent = other.GetComponentInParent<CombatAgent>();
            if (opponent != null)
            {
                Debug.Log("Found combat agent");
                opponent.GetHit(attack);
            }
        } else if (other.CompareTag("Destructible"))
        {
            Debug.Log("Hit destructible object");
            other.GetComponent<Rigidbody>().AddRelativeTorque(gameObject.transform.forward * torqueFactor * -1f, ForceMode.Impulse);
            other.GetComponent<DeathFader>().enabled = true;
        }
    }
}
