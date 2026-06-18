using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using UnityEngine;

public class ExplosiveBarrelSkill : MonoBehaviour, ISkill
{
    private int _damage;
    private float _fuseDuration;
    private float _radius;


    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    public void Init(int damage, float fuseDuration, float radius)
    {
        _damage = damage;
        _fuseDuration = fuseDuration;
        _radius = radius;

        _collider.radius = radius;
    }

    private void Update()
    {
        _fuseDuration -= Time.deltaTime;

        if (_fuseDuration <= 0)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (Collider2D hit in hits)
        {
            if (hit.transform.root.TryGetComponent<EnemyUnitController>(out EnemyUnitController enemy))
            {
                enemy.Health.TakeDamage(_damage);

            }
        }
    }
}
