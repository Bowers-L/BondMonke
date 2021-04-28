using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObjects/AttackInfo", order = 1)]
public class AttackInfo : ScriptableObject
{
    public string attackName;
    public int damage;
    public float staminaCost;
    public float staminaPenaltyOnGuard;
    public bool breaksGuard;
    public float force;
}
