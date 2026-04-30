using Cysharp.Threading.Tasks;
using Data;
using Infrastructure;
using Infrastructure.Events;
using Infrastructure.Services;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class MainMenuPresenter : IStartable, IDisposable
    {
        private MainMenuView _view;
        private SceneController _sceneController;

        public MainMenuPresenter(MainMenuView view, SceneController sceneController, IEventBus eventBus, GameplayRegistry gameplayRegistry)
        {
            _view = view;
            _sceneController = sceneController;
        }

        public void Start()
        {
            _view.StartRequested += OnStartRequested;
            _view.ExitRequested += OnExitRequested;
        }

        public void Dispose()
        {
            _view.StartRequested -= OnStartRequested;
            _view.ExitRequested -= OnExitRequested;
        }

        private void OnStartRequested()
        {
            _sceneController.ChangeScene(3).Forget();
        }

        private void OnExitRequested()
        {
            _sceneController.Exit();
        }
    }
}