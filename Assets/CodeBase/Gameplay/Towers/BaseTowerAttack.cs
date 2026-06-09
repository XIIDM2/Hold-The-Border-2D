using Core.Utilities.CustomProperties;
using Data;
using Gameplay.Towers.TargetSelectionStrategies;
using Infrastructure.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Towers
{
    public abstract class BaseTowerAttack : MonoBehaviour
    {
        public IReadOnlyCollection<ITargetable> TargetsInRange => _targetsInRange.Values;

        private const float TIME_TO_UPDATE_STRATEGY = 0.1f;

        private const int CLOSEST_TO_TOWER_STRATEGY_INDEX = 1;
        private const int CLOSEST_TO_BASE_STRATEGY_INDEX = 2;
        private const int LOWEST_HEALTH_STRATEGY_INDEX = 3;

        [SerializeField, ReadOnly] protected int _damage;
        [SerializeField, ReadOnly] protected float _cooldown;

        protected Dictionary<IDamageable, ITargetable> _targetsInRange = new Dictionary<IDamageable, ITargetable>();
        protected Dictionary<int, ITowerSelectionTargetStrategy> _targetStrategies;

        protected ITargetable _currentTarget;

        protected Coroutine _attackCoroutine;

        private ITowerSelectionTargetStrategy _currentStrategy;
        private float _updateStrategyTimer;

        private void Update()
        {
            if (_targetsInRange.Count == 0) return;

            _updateStrategyTimer += Time.deltaTime;

            if (_currentTarget == null || _updateStrategyTimer > TIME_TO_UPDATE_STRATEGY)
            {
                SelectTarget();
                _updateStrategyTimer = 0;
            }
        }

        public virtual void Init(TowerData.TowerTiersConfig currentTier)
        {
            _damage = currentTier.Damage;
            _cooldown = currentTier.AttackCooldown;

            _targetStrategies = new Dictionary<int, ITowerSelectionTargetStrategy>
            {
                { CLOSEST_TO_TOWER_STRATEGY_INDEX, new ClosestToTowerStrategy(transform.position) },
                { CLOSEST_TO_BASE_STRATEGY_INDEX, new ClosestToBaseStrategy() },
                { LOWEST_HEALTH_STRATEGY_INDEX, new LowestHealthStrategy() },
            };


            if (_currentStrategy == null)
            {
                SelectStrategy(CLOSEST_TO_TOWER_STRATEGY_INDEX);
            }
        }

        public void SelectStrategy(int strategyIndex)
        {
            if (_targetStrategies.TryGetValue(strategyIndex, out ITowerSelectionTargetStrategy targetStrategy))
            {
                _currentStrategy = targetStrategy;
                SelectTarget();
            }
            else Debug.LogWarning("Strategy Index not found");
        }

        public void SelectTarget()
        {
            if (_currentStrategy != null) _currentTarget = _currentStrategy.SelectTarget(_targetsInRange.Values);
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
            if (_targetsInRange.TryAdd(target.Health, target)) target.Health.Death += OnTargetDeath;
        }

        public void RemoveFromAttackList(ITargetable target)
        {
            target.Health.Death -= OnTargetDeath;
            if (_currentTarget == target) _currentTarget = null;
            _targetsInRange.Remove(target.Health);
        }

        protected void OnTargetDeath(IDamageable damageable)
        {

            if (_targetsInRange.TryGetValue(damageable, out ITargetable target))
            {
                RemoveFromAttackList(target);
            }
        }

        protected abstract IEnumerator AttackRoutine();
    }
}