using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTowerAttack : MonoBehaviour
{
    public IReadOnlyList<ITargetable> UnitsToAttack => _unitsToAttack;

    [SerializeField] protected Projectile testPrefab;
    [SerializeField, ReadOnly] protected int _damage;
    [SerializeField, ReadOnly] protected float _cooldown;

    protected List<ITargetable> _unitsToAttack = new List<ITargetable>();

    protected Dictionary<IDamageable, ITargetable> _targets = new Dictionary<IDamageable, ITargetable>();

    protected Coroutine _attackCoroutine;

    public void Init(int damage, float cooldown)
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

    protected abstract IEnumerator AttackRoutine();

    protected void OnTargetDeath(IDamageable damageable)
    {
        if (_targets.TryGetValue(damageable, out ITargetable target))
        {
            RemoveFromAttackList(target);
        }
    }
}
