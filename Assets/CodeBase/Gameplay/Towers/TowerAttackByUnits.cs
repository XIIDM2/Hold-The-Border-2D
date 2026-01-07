using Data;
using Infrastructure.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Towers
{
    public class TowerAttackByUnits : BaseTowerAttack
    {
        private List<IAttacker> _attackers = new List<IAttacker>();

        public override void Init(int damage, float cooldown)
        {
            base.Init(damage, cooldown);

            _attackers.Clear();

            _attackers = GetComponentsInChildren<IAttacker>().ToList();

            foreach (IAttacker attacker in _attackers)
            {
                attacker.Init(damage, cooldown);
            }
        }

        protected override IEnumerator AttackRoutine()
        {
            while (_unitsToAttack.Count > 0)
            {
                foreach (IAttacker attacker in _attackers)
                {
                    attacker.ExecuteAttack();
                }

                yield return new WaitForSeconds(_cooldown);
            }
        }


    }
}