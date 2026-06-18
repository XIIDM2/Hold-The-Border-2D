using Gameplay.StatusEffects;
using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class SpikesSkill : MonoBehaviour, ISkill
{
    [SerializeField] private float _tickCooldown = 1f;

    private int _damage;
    private int _slowPercentage;
    private float _duration;

    private Dictionary<EnemyUnitController, OnSpikesEffect> _enemiesUnderEffect = new();

    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    public void Init(int damage, int slowPercentage, float duration, float radius)
    {
        _damage = damage;
        _slowPercentage = slowPercentage;
        _duration = duration;

        _collider.radius = radius;

        Destroy(gameObject, _duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.TryGetComponent<EnemyUnitController>(out EnemyUnitController enemy))
        {
            OnSpikesEffect onSpikesEffect = new OnSpikesEffect(_tickCooldown, _damage, _slowPercentage);
            enemy.AddStatusEffect(onSpikesEffect);

            _enemiesUnderEffect.TryAdd(enemy, onSpikesEffect);
            enemy.Removed += OnRemoved;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.root.TryGetComponent<EnemyUnitController>(out EnemyUnitController enemy))
        {
            if (_enemiesUnderEffect.TryGetValue(enemy, out OnSpikesEffect effect)) enemy.RemoveStatusEffect(effect);

            _enemiesUnderEffect.Remove(enemy);
            enemy.Removed -= OnRemoved;
        }
    }

    private void OnRemoved(EnemyUnitController enemy)
    {
        enemy.Removed -= OnRemoved;
        _enemiesUnderEffect.Remove(enemy);
    }


    
}
