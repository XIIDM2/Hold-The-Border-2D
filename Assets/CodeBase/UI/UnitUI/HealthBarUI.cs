using DG.Tweening;
using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

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
        SetHealthBarValue(_health.MaxHealth);
        _damagedBar.fillAmount = _healthBar.fillAmount;
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

        _damagedBar.DOFillAmount(_healthBar.fillAmount, _fillSpeed).SetDelay(_fillDelay).SetLink(gameObject, LinkBehaviour.KillOnDisable);
    }

}
