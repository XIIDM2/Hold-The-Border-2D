using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    public int PathEndDamage => _pathEndDamage;
    public int AttackDamage => _damage;
    public float AttackCooldown => _attackCooldown;

    [SerializeField, ReadOnly] private int _pathEndDamage;
    [SerializeField, ReadOnly] private int _damage;
    [SerializeField, ReadOnly] private float _attackCooldown;

    public void Init(int pathEndDamage, int attackDamage, float attackCooldown)
    {
        _pathEndDamage = pathEndDamage;
        _damage = attackDamage;
        _attackCooldown = attackCooldown;
    }

    public void Attack()
    {

    }
}
