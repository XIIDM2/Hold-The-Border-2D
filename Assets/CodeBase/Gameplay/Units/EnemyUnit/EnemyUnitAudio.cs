using Infrastructure.Events;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace Gameplay.Units.Enemy
{
    public class EnemyUnitAudio : MonoBehaviour
    {
        private IDamageable _health;

        private AudioClip _hitSound;
        private AudioClip _deathSound;

        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _health = GetComponent<EnemyUnitController>().Health;
        }


        private void OnEnable()
        {
            _health.HealthChanged += OnHealthChanged;
            _health.Death += OnDeath;

        }

        private void OnDisable()
        {
            _health.HealthChanged -= OnHealthChanged;
            _health.Death -= OnDeath;
        }

        public void Init(AudioClip hitSound, AudioClip deathSound)
        {
            _hitSound = hitSound;
            _deathSound = deathSound;

        }

        private void OnHealthChanged(int _)
        {
            _eventBus.Publish(new InvokeSFX(_hitSound));
        }

        private void OnDeath(IDamageable _)
        {
            _eventBus.Publish(new InvokeSFX(_deathSound));
        }


    }
}