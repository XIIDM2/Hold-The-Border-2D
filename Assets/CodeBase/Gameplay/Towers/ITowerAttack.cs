using UnityEngine;

public interface ITowerAttack : IAttack
{
    void ApplyCurrentTier(int damage, float attackCooldown);
    void AddToAttackList(IDamageable damageable);
    void RemoveFromAttackList(IDamageable damageable);

}
