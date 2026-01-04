using Core.Utilities.CustomProperties;
using Infrastructure.Interfaces;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField, ReadOnly] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _minAttackDistance = 0.1f;


    private ITargetable _target;
    private Vector2 lastTargetPosition;

    private void Update()
    {
        if (_target != null)
        {
            lastTargetPosition = _target.Position;
        }

        Move();
    }

    public void Init(ITargetable target, int damage)
    {
        _target = target;
        _damage = damage;
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, lastTargetPosition, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, lastTargetPosition) < _minAttackDistance)
        {
            ApplyDamageToTarget();
        }
    }

    private void ApplyDamageToTarget()
    {
        if (_target !=  null) _target.Damageable.TakeDamage(_damage);
        Destroy(gameObject);     
    }

}
