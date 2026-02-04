using Core.Utilities;
using Infrastructure.Interfaces;
using Core.Utilities.CustomProperties;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField, ReadOnly] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _minAttackDistance = 0.1f;

    private Vector2 oldPosition;

    private ITargetable _target;
    private Vector2 lastTargetPosition;


    private void Update()
    {
        if (_target as Object != null)
        {
            lastTargetPosition = _target.Position;
        }

        oldPosition = transform.position;
        Move();
        Rotate();
    }

    public void Init(int damage)
    {
        _damage = damage;
    }

    public void SetTarget(ITargetable target)
    {
        _target = target;
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, lastTargetPosition, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, lastTargetPosition) < _minAttackDistance)
        {
            ApplyDamageToTarget();
        }
    }

    private void Rotate()
    {
        Vector2 direction = (Vector2)transform.position - oldPosition;

        float angle = Utilities.GetAngleFromVector(direction);

        transform.eulerAngles = new Vector3(0, 0, angle);

    }

    private void ApplyDamageToTarget()
    {
        if (_target !=  null) _target.Damageable.TakeDamage(_damage);
        Destroy(gameObject);     
    }

}
