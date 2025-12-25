using System.Collections.Generic;
using UnityEngine;

public class TowerAttackByUnits : MonoBehaviour, ITowerAttack
{

    [SerializeField, ReadOnly] private int _damage;
    [SerializeField, ReadOnly] private float _cooldown;

    private List<IDamageable> _unitsToAttack = new List<IDamageable>();

    private float _lastAttackTime;

    public void ApplyCurrentTier(int damage, float Cooldown)
    {
        _damage = damage;
        _cooldown = Cooldown;
    }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (_unitsToAttack.Count > 0)
        {
            if (_unitsToAttack[0] is Component component)
            {
                Debug.Log("Target: " + component.gameObject.name);
            }

            foreach (var unit in _unitsToAttack)
            {
                if (unit is Component cmp)
                {
                    Debug.Log(cmp.gameObject.name);
                }
            }
        }

    }

    public void AddToAttackList(IDamageable damageable)
    {
        _unitsToAttack.Add(damageable);
    }

    public void RemoveFromAttackList(IDamageable damageable)
    {
        _unitsToAttack.Remove(damageable);
    }
}
