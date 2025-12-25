using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    event UnityAction<int> OnHealthChanged;
    event UnityAction<IDamageable> OnDeath;

    public int MaxHealth { get; }
    public int CurrentHealth { get; }

    void Init(int maxHealth);
    void TakeDamage(int amount);
    void HealDamage(int amount);

}
