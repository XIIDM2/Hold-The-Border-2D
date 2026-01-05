using Data;
using Infrastructure.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Towers
{
    public class TowerAttackByUnits : BaseTowerAttack, IAttackerRequireable
    {
        private List<IAttacker> _attackers = new List<IAttacker>();

        public void InitAttackers(GameObject unitPrefab, Vector2[] unitsPositions)
        {
            foreach (IAttacker attacker in _attackers)
            {
                if (attacker is Component component)
                {
                    Destroy(component.gameObject);
                }
            }

            _attackers.Clear();

            foreach (Vector2 unitPosition in unitsPositions)
            {
                GameObject unit = Instantiate(unitPrefab, gameObject.transform, false);

                unit.transform.localPosition = unitPosition;

                if (unit.TryGetComponent<IAttacker>(out IAttacker attacker))
                {
                    _attackers.Add(attacker);
                }
                else
                {
                    Destroy(unit);
                }
            }

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
                    attacker.ExecuteAttack();
                }

                yield return new WaitForSeconds(_cooldown);
            }
        }


    }
}