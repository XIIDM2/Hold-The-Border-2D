using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAttackByUnits : BaseTowerAttack, IAttackerRequireable
{
    private List<IAttacker> _attackers = new List<IAttacker>();

    public void Init(AnimationData animations, int damage, float coolDown)
    {
        _attackers = GetComponentsInChildren<IAttacker>().ToList();

        foreach (IAttacker attacker in _attackers)
        {
            attacker.Init(animations, _damage, _cooldown);
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
