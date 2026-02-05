using DG.Tweening;
using Gameplay.Units.Enemy;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;

        [Header("DamagedBar")]
        [SerializeField] private Image _damagedBar;
        [SerializeField] private float _fillSpeed = 0.2f;
        [SerializeField] private float _fillDelay = 0.2f;

        private IDamageable _health;

        private void Awake()
        {
            _health = transform.root.GetComponent<EnemyUnitController>().Health;
        }

        private void Start()
        {
            _healthSlider.wholeNumbers = true;

            _healthSlider.maxValue = _health.MaxHealth;
            _healthSlider.value = _health.CurrentHealth;

            _damagedBar.fillAmount = _health.CurrentHealth;
        }

        private void OnEnable()
        {
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int amount)
        {
            _healthSlider.value = amount;

            _damagedBar.DOFillAmount(_healthSlider.value / _health.MaxHealth, _fillSpeed).SetDelay(_fillDelay).SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

    }
}