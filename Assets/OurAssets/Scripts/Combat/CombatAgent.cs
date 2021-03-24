using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatAgent : MonoBehaviour
{

    public abstract void TakeDamage();
    public abstract void DealDamage();
}
