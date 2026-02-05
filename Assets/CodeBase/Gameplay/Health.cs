using Infrastructure.Interfaces;
using System;
using UnityEngine.Events;

namespace Gameplay
{
    public class Health : IDamageable
    {
        public event UnityAction<int> HealthChanged;
        public event UnityAction<IDamageable> Death;

        public int MaxHealth { get; private set; }

        private int _currentHealth;

        public int CurrentHealth
        {
            get
            {
                return _currentHealth;
            }
            private set
            {
                _currentHealth = Math.Clamp(value, 0, MaxHealth);
                HealthChanged?.Invoke(_currentHealth);
            }
        }

        public void Initialize(int maxHealth)
        {
            MaxHealth = maxHealth;
            _currentHealth = MaxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (CurrentHealth <= 0) return;

            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
            {
                Death?.Invoke(this);
            }
        }

        public void HealDamage(int amount)
        {
            if (CurrentHealth <= 0 || CurrentHealth >= MaxHealth) return;

            CurrentHealth += amount;
        }
    }
}