using UnityEngine;

public abstract class BaseUnitAttack : MonoBehaviour
{
    public int AttackDamage => _damage;
    public float AttackCooldown => _cooldown;

    [SerializeField, ReadOnly] protected int _damage;
    [SerializeField, ReadOnly] protected float _cooldown;

    public virtual void Init(int damage, float coolDown)
    {
        _damage = damage;
        _cooldown = coolDown;
    }

    public abstract void Attack(ITargetable target);
}
