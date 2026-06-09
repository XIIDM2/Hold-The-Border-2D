using Core.Utilities;
using Data;
using Gameplay.Units;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Towers.Units
{
    public class TowerUnitAnimation : UnitAnimation
    {
        public event UnityAction<Transform> AttackAnimationEvent;
        public UnityAction TowerUnitSpawn;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private AnimationOverrideData _data;

        private Vector2? _targetPosition;

        protected override void Awake()
        {
            base.Awake();

            Init(_data);

        }

        private void Update()
        {
            if (_targetPosition.HasValue)
            {
                Vector2 direction = CalculateDirection();
                Utilities.FlipTransform(gameObject.transform, direction.x);
                SetFloat(HorizontalHash, direction.x);
                SetFloat(VerticalHash, direction.y);
            }
        }

        public void SetTargetPosition (Vector2? targetPosition)
        {
            _targetPosition = targetPosition;
        }

        public void PlayAttackAnimation()
        {
            if (!enabled) return;
            SetTrigger(IsAttackingHash);
        }

        public void AEAttack()
        {
            AttackAnimationEvent?.Invoke(_firePoint);
        }

        private Vector2 CalculateDirection()
        {
            Vector2 direction = (Vector3)_targetPosition.Value - transform.position;

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