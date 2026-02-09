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
            _view.RestartButton.onClick.AddListener(RestartLevel);
            _view.MainMenuButton.onClick.AddListener(ExitToMainMenu);
        }

        public void Dispose()
        {
            _view.PauseButton.onClick.RemoveListener(PauseGame);
            _view.ContinueButton.onClick.RemoveListener(ContinueGame);
            _view.RestartButton.onClick.RemoveListener(RestartLevel);
            _view.MainMenuButton.onClick.RemoveListener(ExitToMainMenu);
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

        private void RestartLevel()
        {
            _controller.StartTime();
            _controller.RestartScene();
        }

        private void ExitToMainMenu()
        {
            _controller.LoadMainMenuScene();
        }
    }
}