using Gameplay.Projectiles;
using Gameplay.Towers.Units;
using Infrastructure.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Towers
{
    public class TowerAttackByUnits : BaseTowerAttack, IAttackerRequireable, IProjectileRequireable
    {
        private GameObject _unitsVisualPrefab;
        private Projectile _projectilePrefab; 

        private List<TowerUnitAnimation> _attackers = new List<TowerUnitAnimation>();

        private TowerAnimation _animation;

        private WaitForSeconds _unitsSharedAttackCooldown;

        private void Awake()
        {
            _animation = GetComponentInChildren<TowerAnimation>();
        }

        public override void Initialize(int damage, float cooldown)
        {
            base.Initialize(damage, cooldown);

            InitializeAttackers();

        }

        public void InitializeUnitVisualPrefab(GameObject prefab)
        {
            Destroy(_unitsVisualPrefab);

            _unitsVisualPrefab = Instantiate(prefab, gameObject.transform);
        }


        public void InitializeAttackers()
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
            _projectilePrefab.Initialize(_damage);
        }

        private void InstantiateProjectile(Transform _firePoint)
        {
            if (_unitsInRange.Count == 0) return;

            Projectile projectile = Instantiate(_projectilePrefab, _firePoint.transform.position, Quaternion.identity);
            projectile.SetTarget(_unitsInRange[0]);

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