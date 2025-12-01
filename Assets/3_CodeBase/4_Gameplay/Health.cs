using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public event UnityAction<int> OnHealthChanged;
    public event UnityAction OnDeath;

    [Header("Parameters")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public int MaxHealth => _maxHealth;

    public int CurrentHealth
    {
        get 
        { 
            return _currentHealth; 
        }
        private set
        {
            _currentHealth = Math.Clamp(value, 0, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }
 
    private void Awake()
    {
        _maxHealth = 10; // change on data later
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
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
        if (CurrentHealth <= 0 || CurrentHealth >= _maxHealth) return;

        CurrentHealth += amount;       
    }
}
