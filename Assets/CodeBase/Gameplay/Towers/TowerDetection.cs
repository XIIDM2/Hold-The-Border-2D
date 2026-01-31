using Core.Utilities.CustomProperties;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Towers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TowerDetection : MonoBehaviour
    {
        public event UnityAction<ITargetable> TargetEntered;
        public event UnityAction<ITargetable> TargetExited;

        [SerializeField, ReadOnly] private float _attackRadius;

        private CircleCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent(out ITargetable target))
            {
                TargetEntered?.Invoke(target);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent(out ITargetable target))
            {
                TargetExited?.Invoke(target);
            }
        }

        public void Init(float radius)
        { 
            _attackRadius = radius;
            _collider.radius = _attackRadius;
        }

        private void OnDrawGizmosSelected()
        {
            if (_collider == null) return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _collider.radius);
        }
    }
}