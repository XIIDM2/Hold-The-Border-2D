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

        public void Init()
        {
            _healthSlider.wholeNumbers = true;

            _healthSlider.maxValue = _health.MaxHealth;
            _healthSlider.value = _health.CurrentHealth;

            _damagedBar.fillAmount = _health.CurrentHealth;

            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _health.HealthChanged += OnHealthChanged;
            _health.Death += OnDeath;
        }

        private void OnDisable()
        {
            _health.HealthChanged -= OnHealthChanged;
            _health.Death -= OnDeath;
        }

        private void OnHealthChanged(int amount)
        {
            _healthSlider.value = amount;

            _damagedBar.DOFillAmount(_healthSlider.value / _health.MaxHealth, _fillSpeed).SetDelay(_fillDelay).SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        private void OnDeath(IDamageable damageable)
        {
            gameObject.SetActive(false);
        }

    }
}