using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackByUnits : MonoBehaviour, ITowerAttack
{
    public IReadOnlyList<ITargetable> UnitsToAttack => _unitsToAttack;

    [SerializeField] Projectile testPrefab;
    [SerializeField, ReadOnly] private int _damage;
    [SerializeField, ReadOnly] private float _cooldown;

    private List<ITargetable> _unitsToAttack = new List<ITargetable>();

    private Dictionary<IDamageable, ITargetable> _targets = new Dictionary<IDamageable, ITargetable>();

    private Coroutine _attackCoroutine;

    public void ApplyCurrentTier(int damage, float cooldown)
    {
        _damage = damage;
        _cooldown = cooldown;
    }

    public void Attack()
    {
        _attackCoroutine ??= StartCoroutine(AttackRoutine());
    }

    public void StopAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;    
        }
    }


    public void AddToAttackList(ITargetable target)
    {
        if (!_unitsToAttack.Contains(target))
        {
            _unitsToAttack.Add(target);
        }

        _targets.TryAdd(target.Damageable, target);

        target.Damageable.OnDeath += OnTargetDeath;
    }

    public void RemoveFromAttackList(ITargetable target)
    {
        target.Damageable.OnDeath -= OnTargetDeath;
        _targets.Remove(target.Damageable);
        _unitsToAttack.Remove(target);
    }

    private IEnumerator AttackRoutine()
    {
        while (_unitsToAttack.Count > 0)
        {
            // logic for archers
            Instantiate(testPrefab, transform.position, Quaternion.identity).Init(_unitsToAttack[0], _damage);
            yield return new WaitForSeconds(_cooldown);
        }
    }

    private void OnTargetDeath(IDamageable damageable)
    {
        if (_targets.TryGetValue(damageable, out ITargetable target))
        {
            RemoveFromAttackList(target);
        }
    }
}
