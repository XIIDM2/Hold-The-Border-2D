using Infrastructure;
using Infrastructure.Services;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class ResultPresenter : IStartable, IDisposable
    {
        private readonly ResultView _view;

        private readonly ILevelService _levelService;
        private readonly SceneController _controller;

        public ResultPresenter(ResultView view, ILevelService manager, SceneController controller)
        {
            _view = view;
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