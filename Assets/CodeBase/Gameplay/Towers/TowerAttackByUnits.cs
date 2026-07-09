using Data;
using Gameplay.Projectiles;
using Gameplay.Towers.Units;
using Infrastructure.Interfaces;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.Towers
{
    public class TowerAttackByUnits : BaseTowerAttack, IAttackerRequireable
    {
        private GameObject _unitsVisualPrefab;
        private Projectile _projectilePrefab;

        private TowerUnitAnimation[] _attackers = Array.Empty<TowerUnitAnimation>();

        private TowerAnimation _animation;
        private TowerAudio _audio;

        private WaitForSeconds _unitsSharedAttackCooldown;

        private ObjectPool<Projectile> _pool;

        private void Awake()
        {
            _animation = GetComponentInChildren<TowerAnimation>();
            _audio = GetComponent<TowerAudio>();

            _pool = new ObjectPool<Projectile>
            (
                createFunc: CreateProjectile,
                actionOnGet: GetProjectile,
                actionOnRelease: ReleaseProjectile,
                actionOnDestroy: DestroyProjectile,
                collectionCheck: true,   
                defaultCapacity: 10,
                maxSize: 50
            );

        }

        private Projectile CreateProjectile()
        {
            return Instantiate(_projectilePrefab);
        }

        private void GetProjectile(Projectile projectile)
        {

            projectile.gameObject.SetActive(true);
        }

        private void ReleaseProjectile(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void DestroyProjectile(Projectile projectile)
        {
            Destroy(projectile);
        }

        public override void Init(TowerData.TowerTiersConfig currentTier)
        {
            base.Init(currentTier);

            InitUnitVisualPrefab(currentTier.AtackersModulePrefab);

            InitProjectile(currentTier.ProjectilePrefab);

            InitAttackers();

        }

        public void InitAttackers()
        {

            foreach (TowerUnitAnimation attacker in _attackers)
            {
                attacker.AttackAnimationEvent -= InstantiateProjectile;
                _animation.UpgradeAnimationCompleted -= attacker.TowerUnitSpawn;
            }

            _attackers = _unitsVisualPrefab.GetComponentsInChildren<TowerUnitAnimation>();

            foreach (TowerUnitAnimation attacker in _attackers)
            {
                attacker.AttackAnimationEvent += InstantiateProjectile;
                _animation.UpgradeAnimationCompleted += attacker.TowerUnitSpawn;
            }

            _unitsSharedAttackCooldown = new WaitForSeconds(_cooldown / _attackers.Length);
        }

        private void InitUnitVisualPrefab(GameObject prefab)
        {
            Destroy(_unitsVisualPrefab);

            _unitsVisualPrefab = Instantiate(prefab, gameObject.transform);
        }
        private void InitProjectile(GameObject projectilePrefab)
        {
            _projectilePrefab = projectilePrefab.GetComponent<Projectile>();
        }

        private void InstantiateProjectile(Transform _firePoint)
        {
            if (_targetsInRange.Count == 0) return;

            Projectile projectile = _pool.Get();

            projectile.Init(_damage);
            projectile.transform.position = _firePoint.transform.position;
            projectile.SetTarget(_currentTarget);

            projectile.InitPool(_pool);

            _audio.OnAttack();

        }

        protected override IEnumerator AttackRoutine()
        {
            if (_attackers.Length == 0) yield break;

            while (_targetsInRange.Count > 0)
            {
                foreach (TowerUnitAnimation attacker in _attackers)
                {
                    if (_targetsInRange.Count == 0) yield break;

                    if (_currentTarget != null && _targetsInRange.ContainsKey(_currentTarget.Health))
                    {
                        attacker.SetTargetPosition(_currentTarget.Position);
                        attacker.PlayAttackAnimation();
                    }
                    else
                    {
                        attacker.SetTargetPosition(null);
                        yield return null;
                        break;
                    }

                    yield return _unitsSharedAttackCooldown;
                }
            }

            _attackCoroutine = null;
        }
    }
}