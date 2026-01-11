using Gameplay.Towers.Units;
using Core.Utilities.CustomProperties;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Towers
{
    public class TowerAttackByUnits : BaseTowerAttack, IAttackerRequireable, IProjectileRequireable
    {
        [SerializeField, ReadOnly] private Projectile _projectilePrefab;

        private List<TowerUnitAnimation> _attackers = new List<TowerUnitAnimation>();
        public override void Init(int damage, float cooldown)
        {
            base.Init(damage, cooldown);

            InitAttackers();

        }

        public void InitProjectile(GameObject projectilePrefab)
        {
            _projectilePrefab = projectilePrefab.GetComponent<Projectile>();
            _projectilePrefab.Init(_damage);
        }

        public void InitAttackers()
        {
            foreach (TowerUnitAnimation attacker in _attackers)
            {
                attacker.AttackAnimationEvent -= InstantiateProjectile;
            }

            _attackers.Clear();

            _attackers = GetComponentsInChildren<TowerUnitAnimation>().ToList();

            foreach (TowerUnitAnimation attacker in _attackers)
            {
                attacker.AttackAnimationEvent += InstantiateProjectile;
            }
        }

        private void InstantiateProjectile()
        {
            Projectile projectile = Instantiate(_projectilePrefab);
            projectile.SetTarget(_unitsToAttack[0]);

        }

        protected override IEnumerator AttackRoutine()
        {
            while (_unitsToAttack.Count > 0)
            {
                foreach (TowerUnitAnimation attacker in _attackers)
                {
                    attacker.PlayAttackAnimation();
                }

                yield return new WaitForSeconds(_cooldown);
            }
        }
    }
}