using Core.Utilities;
using Data;
using Gameplay.Units;
using Infrastructure.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Towers.Units
{
    public class TowerUnitAnimation : UnitAnimation
    {
        public Transform FirePoint => _firePoint;
        public event UnityAction<Transform> AttackAnimationEvent;
        public UnityAction TowerUnitSpawn;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private AnimationOverrideData _data;

        private GameObject _target;

        private SpriteRenderer _sprite;

        protected override void Awake()
        {
            base.Awake();
            _sprite = GetComponent<SpriteRenderer>();
            Init(_data);
        }


        private void Update()
        {
            if (_target)
            {
                Vector2 direction = CalculateDirection();
                Utilities.FlipSprite(_sprite, direction.x);
                SetFloat(UnitAnimationParameter.Horizontal.ToString(), direction.x);
                SetFloat(UnitAnimationParameter.Vertical.ToString(), direction.y);
            }
        }

        public void SetTarget(ITargetable target)
        {
            if (target != null && target is Component component)
            {
                _target = component.gameObject;
            }
        }

        public void PlayAttackAnimation()
        {
            if (!enabled) return; 

            SetTrigger(UnitAnimationParameter.IsAttacking.ToString());
        }

        public void AEAttack()
        {
            AttackAnimationEvent?.Invoke(_firePoint);
        }

        private Vector2 CalculateDirection()
        {
            Vector2 direction = _target.transform.position - transform.position;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                direction = new Vector2(Mathf.Sign(direction.x), 0);
            }
            else
            {
                direction = new Vector2(0, Mathf.Sign(direction.y));
            }

            return direction;
        }
    }
}