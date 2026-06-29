using Data;
using Gameplay.StatusEffects;
using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Skills
{
    public class SpikesSkill : MonoBehaviour, ISkill
    {
        [SerializeField] private float _tickCooldown = 1f;

        private int _damage;
        private int _slowPercentage;

        private Dictionary<EnemyUnitController, OnSpikesEffect> _enemiesUnderEffect = new();

        private CircleCollider2D _collider;

        private SpikesSkillAnimation _animation;

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            _animation = GetComponentInChildren<SpikesSkillAnimation>();
        }

        public void Init(int damage, int slowPercentage, float duration, float radius, AnimationOverrideData _animationData)
        {
            _damage = damage;
            _slowPercentage = slowPercentage;

            _collider.radius = radius;

            _animation.Init(_animationData);

            StartCoroutine(LifeTimeRoutine(duration));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent(out EnemyUnitController enemy))
            {
                OnSpikesEffect onSpikesEffect = new OnSpikesEffect(_tickCooldown, _damage, _slowPercentage);
                enemy.AddStatusEffect(onSpikesEffect);

                _enemiesUnderEffect.TryAdd(enemy, onSpikesEffect);
                enemy.Removed += OnRemoved;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent(out EnemyUnitController enemy))
            {
                if (_enemiesUnderEffect.TryGetValue(enemy, out OnSpikesEffect effect)) enemy.RemoveStatusEffect(effect);

                _enemiesUnderEffect.Remove(enemy);
                enemy.Removed -= OnRemoved;
            }
        }

        private IEnumerator LifeTimeRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);

            _animation.Despawn();
        }

        private void OnRemoved(EnemyUnitController enemy)
        {
            enemy.Removed -= OnRemoved;
            _enemiesUnderEffect.Remove(enemy);
        }



    }
}