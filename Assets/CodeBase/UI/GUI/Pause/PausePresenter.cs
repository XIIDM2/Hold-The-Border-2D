using Infrastructure;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class PausePresenter : IStartable, IDisposable
    {
        private readonly PauseView _view;
        private readonly SceneController _controller;

        public PausePresenter(PauseView view, SceneController controller)
        {
            _view = view;
            _controller = controller;
        }

        public void Start()
        {
            _view.PauseRequested += OnPauseRequested;
            _view.ContinueRequested += OnContinueRequested;
            _view.RestartRequested += OnRestartRequested;
            _view.MainMenuRequested += OnMainMenuRequested;

        }

        public void Dispose()
        {
            _view.PauseRequested -= OnPauseRequested;
            _view.ContinueRequested -= OnContinueRequested;
            _view.RestartRequested -= OnRestartRequested;
            _view.MainMenuRequested -= OnMainMenuRequested;
        }

        private void OnPauseRequested()
        {
            _controller.StopTime();
            _view.ShowPanel();
        }

        private void OnContinueRequested()
        {
            _controller.StartTime();
            _view.HidePanel();
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