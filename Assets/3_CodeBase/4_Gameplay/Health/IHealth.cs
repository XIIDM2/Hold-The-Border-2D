using UnityEngine;
using UnityEngine.Events;

public interface IHealth
{
    event UnityAction<int> OnHealthChanged;
    event UnityAction OnDeath;
    int MaxHealth { get; }
    int CurrentHealth { get; }
    void Init(int maxHealth);
    void TakeDamage(int amount);
    void HealDamage(int amount);

}
