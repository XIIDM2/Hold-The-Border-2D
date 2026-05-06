using Cysharp.Threading.Tasks;
using Data;
using Infrastructure;
using Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Threading;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class MainMenuPresenter : IAsyncStartable, IDisposable
    {
        private MainMenuView _view;
        private SceneController _sceneController;

        private readonly IUIFactory _factory;
        private readonly ProjectRegistry _projectRegistry;
        private readonly CancellationToken _ctc;

        private List<LevelButton> _buttons = new List<LevelButton>();

        public MainMenuPresenter(MainMenuView view, SceneController sceneController, IUIFactory factory, ProjectRegistry projectRegistry, CancellationToken cancellationToken)
        {
            _view = view;
            _sceneController = sceneController;
            _factory = factory;
            _projectRegistry = projectRegistry;
            _ctc = cancellationToken;
        }

        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            _view.HideLevelPanel();

            foreach (LevelData data in _projectRegistry.LevelDatas)
            {
                LevelButton button = await _factory.CreateLevelButton(data.LevelName, data.AddressablesLabel, _ctc);

                button.gameObject.transform.SetParent(_view.LevelPanel.transform);

                button.ButtonClicked += OnLevelButtonClicked;

                _buttons.Add(button);
            }

            _view.StartRequested += OnStartRequested;
            _view.ReturnToMenuRequested += OnReturnToMenuRequested;
            _view.ExitRequested += OnExitRequested;
        }

        public void Dispose()
        {
            foreach (LevelButton button in _buttons)
            {
                button.ButtonClicked -= OnLevelButtonClicked;
            }

            _view.StartRequested -= OnStartRequested;
            _view.ReturnToMenuRequested -= OnReturnToMenuRequested;
            _view.ExitRequested -= OnExitRequested;
        }

        private void OnStartRequested()
        {
            _view.ShowLevelPanel();
        }

        private void OnReturnToMenuRequested()
        {
            _view.HideLevelPanel();
        }

        private void OnLevelButtonClicked(string levelName, string addressablesLabel)
        {
            _sceneController.ChangeScene(levelName, addressablesLabel).Forget();
        }

        private void OnExitRequested()
        {
            _sceneController.Exit();
        }

    }
}