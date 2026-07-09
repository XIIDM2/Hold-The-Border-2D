using Infrastructure;
using Infrastructure.Events;
using Infrastructure.Services;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class ResultPresenter : IStartable, IDisposable
    {
        private readonly ResultView _view;

        private readonly IEventBus _eventBus;

        private readonly ILevelService _levelService;
        private readonly SceneController _controller;

        public ResultPresenter(ResultView view, IEventBus eventBus, ILevelService manager, SceneController controller)
        {
            _view = view;
            _eventBus = eventBus;
            _levelService = manager;
            _controller = controller;
        }

        public void Start()
        {
            _levelService.Victory += OnVictory;
            _levelService.Defeat += OnDefeat;

            _view.RestartRequested += OnRestartRequested;
            _view.MainMenuRequested += OnMainMenuRequested;
        }

        public void Dispose()
        {
            _levelService.Victory -= OnVictory;
            _levelService.Defeat -= OnDefeat;

            _view.RestartRequested -= OnRestartRequested;
            _view.MainMenuRequested -= OnMainMenuRequested;
        }

        private void OnVictory()
        {
            _eventBus.Publish(new UIStateChanged(UIStates.InResultPanel));
            _view.ShowVictoryPanel();
        }

        private void OnDefeat()
        {
            _eventBus.Publish(new UIStateChanged(UIStates.InResultPanel));
            _view.ShowDefeatPanel();
        }

        private void OnRestartRequested()
        {
            _controller.RestartScene();
        }

        private void OnMainMenuRequested()
        {
            _controller.LoadMainMenuScene();
        }

    }
}