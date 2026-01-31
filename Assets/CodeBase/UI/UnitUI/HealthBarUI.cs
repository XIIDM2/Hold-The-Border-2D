using DG.Tweening;
using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;

    [Header("DamagedBar")]
    [SerializeField] private Image _damagedBar;
    [SerializeField] private float _fillSpeed = 0.2f;
    [SerializeField] private float _fillDelay = 0.2f;

    private IDamageable _health;

    private void Awake()
    {
        _health = transform.root.GetComponent<EnemyUnitController>().Damageable;
    }

    private void Start()
    {
        _healthBar.wholeNumbers = true;

        _healthBar.maxValue = _health.MaxHealth;
        _healthBar.value = _health.CurrentHealth;

        _damagedBar.fillAmount = _health.CurrentHealth;
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
        _healthBar.value = amount;

        _damagedBar.DOFillAmount(_healthBar.value / _health.MaxHealth, _fillSpeed).SetDelay(_fillDelay).SetLink(gameObject, LinkBehaviour.KillOnDisable);
    }

}
