using Gameplay.Player;
using TMPro;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        private IPlayerController _player;

        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _goldText;

        [Inject]
        public void Contstruct(IPlayerController player)
        {
            _player = player;
        }

        private void Start()
        {
            OnHealthChanged(_player.Health.CurrentHealth);
            OnGoldChanged(_player.Gold);
        }

        private void OnEnable()
        {
            _player.Health.HealthChanged += OnHealthChanged;
            _player.GoldChanged += OnGoldChanged;
        }

        private void OnDisable()
        {
            _player.Health.HealthChanged -= OnHealthChanged;
            _player.GoldChanged -= OnGoldChanged;
        }

        private void OnHealthChanged(int health)
        {
            _healthText.text = health.ToString();
        }

        private void OnGoldChanged(int gold)
        {
            _goldText.text = gold.ToString();
        }


    }
}