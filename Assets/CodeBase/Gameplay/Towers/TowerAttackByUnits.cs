using Gameplay.Towers.Units;
using Core.Utilities.CustomProperties;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Infrastructure.Interfaces;

namespace Gameplay.Towers
{
    public class TowerAttackByUnits : BaseTowerAttack, IAttackerRequireable, IProjectileRequireable
    {
        [SerializeField, ReadOnly] private Projectile _projectilePrefab;
        private GameObject _towerUnitVisualPrefab;
        private GameObject towerUnitVisual;

        private List<TowerUnitAnimation> _attackers = new List<TowerUnitAnimation>();

        private TowerAnimation _animation;

        private WaitForSeconds _timerUnitsAttack;

        private void Awake()
        {
            _animation = GetComponentInChildren<TowerAnimation>();
        }

        public override void Init(int damage, float cooldown)
        {
            base.Init(damage, cooldown);

            InitAttackers();

        }

        public void InitTowerUnitVisualPrefab(GameObject prefab)
        {
            Destroy(towerUnitVisual);

            _towerUnitVisualPrefab = prefab;

            towerUnitVisual = Instantiate(_towerUnitVisualPrefab, gameObject.transform);
        }


        public void InitAttackers()
        {

            foreach (TowerUnitAnimation attacker in _attackers)
            {
                attacker.AttackAnimationEvent -= InstantiateProjectile;
                _animation.UpgradeAnimationCompleted -= attacker.TowerUnitSpawn;
            }

            _attackers.Clear();

            _attackers = towerUnitVisual.GetComponentsInChildren<TowerUnitAnimation>().ToList();

            foreach (TowerUnitAnimation attacker in _attackers)
            {
                attacker.AttackAnimationEvent += InstantiateProjectile;
                _animation.UpgradeAnimationCompleted += attacker.TowerUnitSpawn;
            }

            _timerUnitsAttack = new WaitForSeconds(_cooldown / _attackers.Count);
        }

        public void InitProjectile(GameObject projectilePrefab)
        {
            _projectilePrefab = projectilePrefab.GetComponent<Projectile>();
            _projectilePrefab.Init(_damage);
        }

        private void InstantiateProjectile(Transform _firePoint)
        {
            if (_unitsToAttack.Count == 0) return;

            Projectile projectile = Instantiate(_projectilePrefab, _firePoint.transform.position, Quaternion.identity);
            projectile.SetTarget(_unitsToAttack[0]);

        }

        protected override IEnumerator AttackRoutine()
        {
            if (_attackers.Count == 0) yield break;

            while (_unitsToAttack.Count > 0)
            {
                foreach (TowerUnitAnimation attacker in _attackers)
                {
                    if (_unitsToAttack.Count == 0) yield break;

                    attacker.SetTarget(_unitsToAttack[0]);
                    attacker.PlayAttackAnimation();

                    yield return _timerUnitsAttack;
                }
            }
        }
    }
}