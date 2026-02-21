using Gameplay.Projectiles;
using Gameplay.Towers.Units;
using Infrastructure.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.Towers
{
    public class TowerAttackByUnits : BaseTowerAttack, IAttackerRequireable, IProjectileRequireable
    {
        private GameObject _unitsVisualPrefab;
        private Projectile _projectilePrefab; 

        private List<TowerUnitAnimation> _attackers = new List<TowerUnitAnimation>();

        private TowerAnimation _animation;

        private WaitForSeconds _unitsSharedAttackCooldown;

        private ObjectPool<Projectile> _pool;

        private void Awake()
        {
            _animation = GetComponentInChildren<TowerAnimation>();
            _pool = new ObjectPool<Projectile>
            (
                createFunc: CreateProjectile,
                actionOnGet: OnGetProjectile,
                actionOnRelease: OnReleaseProjectile,
                actionOnDestroy: OnDestroyProjectile,
                collectionCheck: true,   
                defaultCapacity: 10,
                maxSize: 50
            );

        }

        private Projectile CreateProjectile()
        {
            return Instantiate(_projectilePrefab);
        }

        private void OnGetProjectile(Projectile projectile)
        {

            projectile.gameObject.SetActive(true);
        }

        private void OnReleaseProjectile(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnDestroyProjectile(Projectile projectile)
        {
            Destroy(projectile);
        }

        public override void Init(int damage, float cooldown)
        {
            base.Init(damage, cooldown);

            InitAttackers();

        }

        public void InitUnitVisualPrefab(GameObject prefab)
        {
            Destroy(_unitsVisualPrefab);

            _unitsVisualPrefab = Instantiate(prefab, gameObject.transform);
        }

        public void InitAttackers()
        {

            foreach (TowerUnitAnimation attacker in _attackers)
            {
                attacker.AttackAnimationEvent -= InstantiateProjectile;
                _animation.UpgradeAnimationCompleted -= attacker.TowerUnitSpawn;
            }

            _attackers.Clear();

            _attackers = _unitsVisualPrefab.GetComponentsInChildren<TowerUnitAnimation>().ToList();

            foreach (TowerUnitAnimation attacker in _attackers)
            {
                attacker.AttackAnimationEvent += InstantiateProjectile;
                _animation.UpgradeAnimationCompleted += attacker.TowerUnitSpawn;
            }

            _unitsSharedAttackCooldown = new WaitForSeconds(_cooldown / _attackers.Count);
        }

        public void InitProjectile(GameObject projectilePrefab)
        {
            _projectilePrefab = projectilePrefab.GetComponent<Projectile>();
            _projectilePrefab.Init(_damage);
        }

        private void InstantiateProjectile(Transform _firePoint)
        {
            if (_unitsInRange.Count == 0) return;

            Projectile projectile = _pool.Get();
            projectile.transform.position = _firePoint.transform.position;
            projectile.SetTarget(_unitsInRange[0]);

            projectile.InitPool(_pool);

        }

        protected override IEnumerator AttackRoutine()
        {
            if (_attackers.Count == 0) yield break;

            while (_unitsInRange.Count > 0)
            {
                foreach (TowerUnitAnimation attacker in _attackers)
                {
                    if (_unitsInRange.Count == 0) yield break;

                    attacker.SetTarget(_unitsInRange[0]);
                    attacker.PlayAttackAnimation();

                    yield return _unitsSharedAttackCooldown;
                }
            }
        }
    }
}