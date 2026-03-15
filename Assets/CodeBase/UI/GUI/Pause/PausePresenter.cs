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
            _view.PauseButton.onClick.AddListener(PauseGame);
            _view.ContinueButton.onClick.AddListener(ContinueGame);
            _view.RestartButton.onClick.AddListener(_controller.RestartScene);
            _view.MainMenuButton.onClick.AddListener(_controller.LoadMainMenuScene);
        }

        public void Dispose()
        {
            _view.PauseButton.onClick.RemoveListener(PauseGame);
            _view.ContinueButton.onClick.RemoveListener(ContinueGame);
            _view.RestartButton.onClick.RemoveListener(_controller.RestartScene);
            _view.MainMenuButton.onClick.RemoveListener(_controller.LoadMainMenuScene);
        }

        private void PauseGame()
        {
            _controller.StopTime();
            _view.ShowPanel();
        }

        private void ContinueGame()
        {
            _controller.StartTime();
            _view.HidePanel();
        }
    }
}