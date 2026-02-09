using Gameplay.Player;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class PlayerStatsPresenter : IStartable, IDisposable
    {
        private readonly PlayerStatsView _view;
        private readonly IPlayerController _controller;

        public PlayerStatsPresenter(PlayerStatsView view, IPlayerController controller)
        {
            _view = view;
            _controller = controller;
        }

        public void Start()
        {
            _view.Init(_controller.Health.CurrentHealth, _controller.Gold);

            _controller.Health.HealthChanged += _view.OnHealthChanged;
            _controller.GoldChanged += _view.OnGoldChanged;
        }

        public void Dispose()
        {
            _controller.Health.HealthChanged -= _view.OnHealthChanged;
            _controller.GoldChanged -= _view.OnGoldChanged;
        }
    }
}