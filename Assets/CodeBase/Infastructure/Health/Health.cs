using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : IHealth
{
    public event UnityAction<int> OnHealthChanged;
    public event UnityAction OnDeath;

    public int MaxHealth {  get; private set; }

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
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

    public void Init(int maxHealth)
    {
        MaxHealth = maxHealth;
        _currentHealth = MaxHealth;
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage(int amount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void HealDamage(int amount)
    {
        if (CurrentHealth <= 0 || CurrentHealth >= MaxHealth) return;

        CurrentHealth += amount;       
    }
}
