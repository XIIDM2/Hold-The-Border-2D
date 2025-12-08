
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    public int PathEndDamage => _pathEndDamage;
    public int AttackDamage => _attackDamage;

    [SerializeField, ReadOnly] private int _pathEndDamage;
    [SerializeField, ReadOnly] private int _attackDamage;

    public void Init(int pathEndDamage, int attackDamage)
    {
        _pathEndDamage = pathEndDamage;
        _attackDamage = attackDamage;
    }

    public void Attack()
    {

    }
}
