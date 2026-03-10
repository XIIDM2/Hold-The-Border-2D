using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.Managers;
using System;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
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
            _manager.Victory += _view.ShowVictoryPanel;
            _manager.Defeat += _view.ShowDefeatPanel;

            _view.RestartButton.onClick.AddListener(RestartLevel);    
            _view.MainMenuButton.onClick.AddListener(ExitToMainMenu);    
        }

        public void Dispose()
        {
            _manager.Victory -= _view.ShowVictoryPanel;
            _manager.Defeat -= _view.ShowDefeatPanel;

            _view.RestartButton.onClick.RemoveListener(RestartLevel);
            _view.MainMenuButton.onClick.RemoveListener(ExitToMainMenu);
        }

        private void RestartLevel()
        {
            _controller.StartTime();
            _controller.ChangeScene(SceneManager.GetActiveScene().buildIndex).Forget();
        }

        private void ExitToMainMenu()
        {
            _controller.LoadMainMenuScene();
        }
    }
}