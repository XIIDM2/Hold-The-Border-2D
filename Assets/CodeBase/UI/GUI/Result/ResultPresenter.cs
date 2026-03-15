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
            _manager.Victory += _view.ShowVictoryPanel;
            _manager.Defeat += _view.ShowDefeatPanel;

            _view.RestartButton.onClick.AddListener(_controller.RestartScene);    
            _view.MainMenuButton.onClick.AddListener(_controller.LoadMainMenuScene);    
        }

        public void Dispose()
        {
            _manager.Victory -= _view.ShowVictoryPanel;
            _manager.Defeat -= _view.ShowDefeatPanel;

            _view.RestartButton.onClick.RemoveListener(_controller.RestartScene);
            _view.MainMenuButton.onClick.RemoveListener(_controller.LoadMainMenuScene);
        }

    }
}