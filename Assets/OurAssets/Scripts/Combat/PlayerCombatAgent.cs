using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.OurAssets.Scripts.Combat
{
    class PlayerCombatAgent : CombatAgent
    {
        public override void TakeDamage(int damage)
        {
            GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
