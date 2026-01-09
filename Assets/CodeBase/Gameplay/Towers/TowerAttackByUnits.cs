using Data;
using Infrastructure.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Towers
{
    public class TowerAttackByUnits : BaseTowerAttack, IAttackerRequireable
    {
        private List<IAttacker> _attackers = new List<IAttacker>();

        public override void Init(int damage, float cooldown)
        {
            base.Init(damage, cooldown);

            InitAttackers();

        }

        public void InitAttackers()
        {
            _attackers.Clear();

            _attackers = GetComponentsInChildren<IAttacker>().ToList();

            foreach (IAttacker attacker in _attackers)
            {
                attacker.Init(_damage, _cooldown);
            }
        }

        protected override IEnumerator AttackRoutine()
        {
            while (_unitsToAttack.Count > 0)
            {
                foreach (IAttacker attacker in _attackers)
                {
                    attacker.ExecuteAttack(_unitsToAttack[0]);
                }

                yield return new WaitForSeconds(_cooldown);
            }
        }


    }
}