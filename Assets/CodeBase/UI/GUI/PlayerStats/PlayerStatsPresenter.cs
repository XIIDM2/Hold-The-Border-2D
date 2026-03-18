using Gameplay.Player;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class PlayerStatsPresenter : IStartable, IDisposable
    {
        private readonly PlayerStatsView _view;
        private readonly IPlayerController _player;

        public PlayerStatsPresenter(PlayerStatsView view, IPlayerController controller)
        {
            _view = view;
            _player = controller;
        }

        public void Start()
        {
            _view.Init(_player.Health.CurrentHealth, _player.Gold);

            _player.Health.HealthChanged += OnHealthChanged;
            _player.GoldChanged += OnGoldChanged;
        }

        public void Dispose()
        {
            _player.Health.HealthChanged -= OnHealthChanged;
            _player.GoldChanged -= OnGoldChanged;
        }

        private void OnHealthChanged(int amount)
        {
            _view.OnHealthChanged(amount);
        }

        private void OnGoldChanged(int amount)
        {
            _view.OnGoldChanged(amount);
        }
    }
}