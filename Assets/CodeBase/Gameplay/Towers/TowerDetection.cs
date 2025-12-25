using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class TowerDetection : MonoBehaviour
{
    public event UnityAction<IDamageable> TargetEntered;
    public event UnityAction<IDamageable> TargetExited;

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
        if (collision.transform.root.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            TargetEntered?.Invoke(damageable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.root.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            TargetExited?.Invoke(damageable);
        }
    }

    public void ApplyCurrentTier(float radius)
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
