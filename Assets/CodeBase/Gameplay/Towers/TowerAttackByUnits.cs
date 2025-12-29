using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAttackByUnits : BaseTowerAttack
{
    private List<IAttacker> _attackers = new List<IAttacker>();

    private void Start()
    {
        _attackers = GetComponentsInChildren<IAttacker>().ToList();
    }

    protected override IEnumerator AttackRoutine()
    {
        while (_unitsToAttack.Count > 0)
        {
            // logic for archers
            foreach (IAttacker attacker in _attackers)
            {
                attacker.RequestAttack();
            }

            yield return new WaitForSeconds(_cooldown);
        }
    }
}
