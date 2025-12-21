using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class TowerDetection : MonoBehaviour
{
    public event UnityAction<IAttackable> TargetEntered;
    public event UnityAction<IAttackable> TargetExited;

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
        if (collision.TryGetComponent<IAttackable>(out IAttackable attackable))
        {
            TargetEntered?.Invoke(attackable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IAttackable>(out IAttackable attackable))
        {
            TargetExited?.Invoke(attackable);
        }
    }

    public void Init(float radius)
    {
        _collider.radius = radius;
    }

    private void OnDrawGizmosSelected()
    {
        if (_collider == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _collider.radius);
    }
}
