using UnityEngine.Events;

namespace Infrastructure.Interfaces
{
    public interface IDamageable
    {
        event UnityAction<int> HealthChanged;
        event UnityAction<IDamageable> Death;

        public int MaxHealth { get; }
        public int CurrentHealth { get; }

        void Initialize(int maxHealth);
        void TakeDamage(int amount);
        void HealDamage(int amount);

    }
}