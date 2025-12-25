using System.Collections.Generic;
using UnityEngine;

public interface ITowerAttack : IAttack
{
    IReadOnlyList<ITargetable> UnitsToAttack { get; }

    void ApplyCurrentTier(int damage, float attackCooldown);
    void AddToAttackList(ITargetable target);
    void RemoveFromAttackList(ITargetable target);
}
