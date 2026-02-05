using Core.Utilities;
using Core.Utilities.CustomProperties;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Gameplay.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _minAttackDistance = 0.1f;

        private ITargetable _target;
        private Vector2 _lastTargetPosition;
        private Vector2 _oldPosition;


        private void Update()
        {
            if (_target as Object != null)
            {
                _lastTargetPosition = _target.Position;
            }

            _oldPosition = transform.position;
            Move();
            Rotate();
        }

        public void Initialize(int damage)
        {
            _damage = damage;
        }

        public void SetTarget(ITargetable target)
        {
            _target = target;
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, _lastTargetPosition, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _lastTargetPosition) < _minAttackDistance)
            {
                ApplyDamageToTarget();
            }
        }

        private void Rotate()
        {
            Vector2 direction = (Vector2)transform.position - _oldPosition;

            float angle = Utilities.GetAngleFromVector(direction);

            transform.eulerAngles = new Vector3(0, 0, angle);

        }

        private void ApplyDamageToTarget()
        {
            _target?.Health.TakeDamage(_damage);
            Destroy(gameObject);
        }

    }
}