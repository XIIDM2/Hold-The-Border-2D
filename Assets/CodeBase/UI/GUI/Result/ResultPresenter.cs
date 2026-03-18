using Infrastructure;
using Infrastructure.Managers;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class ResultPresenter : IStartable, IDisposable
    {
        private readonly ResultView _view;

        private readonly ILevelManager _manager;
        private readonly SceneController _controller;

        public ResultPresenter(ResultView view, ILevelManager manager, SceneController controller)
        {
            _view = view;
            _manager = manager;
            _controller = controller;
        }

        public void Start()
        {
            _manager.Victory += OnVictory;
            _manager.Defeat += OnDefeat;

            _view.RestartRequested += OnRestartRequested;
            _view.MainMenuRequested += OnMainMenuRequested;
        }

        public void Dispose()
        {
            _manager.Victory -= OnVictory;
            _manager.Defeat -= OnDefeat;

            _view.RestartRequested -= OnRestartRequested;
            _view.MainMenuRequested -= OnMainMenuRequested;
        }

        private void OnVictory()
        {
            _view.ShowVictoryPanel();
        }

        private void OnDefeat()
        {
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