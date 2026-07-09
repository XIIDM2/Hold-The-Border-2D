using Data;
using Gameplay.Units.Enemy;
using Infrastructure.Events;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Gameplay.Skills
{
    public class ExplosiveBarrelSkill : MonoBehaviour, ISkill
    {
        [SerializeField] private AudioClip _explosionSound;

        private int _damage;
        private float _radius;

        private const string ENEMY_LAYER_MASK_NAME = "Enemy";
        private List<Collider2D> _enemiesInExplosionRadius = new();
        private ContactFilter2D _enemyFilter;
            
        private CircleCollider2D _collider;
        private ExplosiveBarrelAnimation _animation;

        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            _animation= GetComponentInChildren<ExplosiveBarrelAnimation>();

            _enemyFilter = new ContactFilter2D();
            _enemyFilter.SetLayerMask(LayerMask.GetMask(ENEMY_LAYER_MASK_NAME));
            _enemyFilter.useTriggers = true;
        }

        private void OnEnable()
        {
            _animation.IsExploded += Explode;
        }

        private void OnDisable()
        {
            _animation.IsExploded -= Explode;
        }

        public void Init(int damage, float fuseDuration, float radius, AnimationOverrideData animationData)
        {
            _damage = damage;
            _radius = radius;

            _collider.radius = radius;

            _animation.Init(animationData);

            StartCoroutine(LifeTimeRoutine(fuseDuration));
        }

        private IEnumerator LifeTimeRoutine(float fuseTime)
        {
            yield return new WaitForSeconds(fuseTime);

            _animation.Despawn();
        }

        private void Explode()
        {
            _eventBus.Publish(new InvokeSFX(_explosionSound));

            Physics2D.OverlapCircle(transform.position, _radius, _enemyFilter, _enemiesInExplosionRadius);

            foreach (Collider2D hit in _enemiesInExplosionRadius)
            {
                if (hit.transform.root.TryGetComponent(out EnemyUnitController enemy))
                {
                    enemy.Health.TakeDamage(_damage);

                }
            }
        }
    }
}