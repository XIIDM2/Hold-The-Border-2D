using Gameplay.Units;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Gameplay.Towers.Units
{
    public class RangedUnitAttack : BaseUnitAttack
    {
        public override void Attack(ITargetable target)
        {
            if (target is Component component)
            {
                Debug.Log("Attacking: " + component.gameObject.name);
            }
           
        }
    }
}