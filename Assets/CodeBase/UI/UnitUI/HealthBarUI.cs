using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private IDamageable _health;

    private void Awake()
    {
        _health = transform.root.GetComponent<EnemyUnitController>().Damageable;
    }

    private void Start()
    {
        SetHealthBarValue(_health.MaxHealth);
    }

    private void OnEnable()
    {
        _health.OnHealthChanged += SetHealthBarValue;
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= SetHealthBarValue;
    }

    private void SetHealthBarValue(int amount)
    {
        _healthBar.fillAmount = (float) amount / _health.MaxHealth;
    }

}
