using Core.Utilities.CustomProperties;
using Infrastructure.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Towers
{
    public abstract class BaseTowerAttack : MonoBehaviour
    {
        public IReadOnlyList<ITargetable> UnitsInRange => _unitsInRange;

        [SerializeField, ReadOnly] protected int _damage;
        [SerializeField, ReadOnly] protected float _cooldown;

        protected List<ITargetable> _unitsInRange = new List<ITargetable>();
        protected Dictionary<IDamageable, ITargetable> _targetsInRange = new Dictionary<IDamageable, ITargetable>();

        protected Coroutine _attackCoroutine;

        public virtual void Initialize(int damage, float cooldown)
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
            if (!_unitsInRange.Contains(target))
            {
                _unitsInRange.Add(target);
            }

            _targetsInRange.TryAdd(target.Health, target);

            target.Health.Death += OnTargetDeath;
        }

        public void RemoveFromAttackList(ITargetable target)
        {
            target.Health.Death -= OnTargetDeath;
            _targetsInRange.Remove(target.Health);
            _unitsInRange.Remove(target);
        }

        protected abstract IEnumerator AttackRoutine();

        protected void OnTargetDeath(IDamageable damageable)
        {
            if (_targetsInRange.TryGetValue(damageable, out ITargetable target))
            {
                RemoveFromAttackList(target);
            }
        }
    }
}