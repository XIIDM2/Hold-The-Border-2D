using Data;
using Gameplay.Units.Enemy;
using Infrastructure.Events;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using System.Collections;
using UnityEngine;
using VContainer;

namespace Gameplay.Skills
{
    public class ExplosiveBarrelSkill : MonoBehaviour, ISkill
    {
        [SerializeField] private AudioClip _explosionSound;

        private int _damage;
        private float _radius;

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

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius);

            foreach (Collider2D hit in hits)
            {
                if (hit.transform.root.TryGetComponent(out EnemyUnitController enemy))
                {
                    enemy.Health.TakeDamage(_damage);

                }
            }
        }
    }
}