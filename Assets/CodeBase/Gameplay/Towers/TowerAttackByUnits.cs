using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackByUnits : BaseTowerAttack
{
    protected override IEnumerator AttackRoutine()
    {
        while (_unitsToAttack.Count > 0)
        {
            // logic for archers
            Instantiate(testPrefab, transform.position, Quaternion.identity).Init(_unitsToAttack[0], _damage);
            yield return new WaitForSeconds(_cooldown);
        }
    }
}
