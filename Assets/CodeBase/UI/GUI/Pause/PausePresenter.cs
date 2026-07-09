using Infrastructure;
using Infrastructure.Events;
using Infrastructure.Services;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class PausePresenter : IStartable, IDisposable
    {
        private readonly PauseView _view;
        private readonly SceneController _controller;

        private readonly IEventBus _eventBus;

        public PausePresenter(PauseView view, SceneController controller, IEventBus eventBus)
        {
            _view = view;
            _controller = controller;
            _eventBus = eventBus;
        }

        public void Start()
        {
            _view.PauseRequested += OnPauseRequested;
            _view.ContinueRequested += OnContinueRequested;
            _view.RestartRequested += OnRestartRequested;
            _view.MainMenuRequested += OnMainMenuRequested;

            _eventBus.Subscribe<UIStateChanged>(OnUIStateChanged);

        }

        public void Dispose()
        {
            _view.PauseRequested -= OnPauseRequested;
            _view.ContinueRequested -= OnContinueRequested;
            _view.RestartRequested -= OnRestartRequested;
            _view.MainMenuRequested -= OnMainMenuRequested;

            _eventBus.Unsubscribe<UIStateChanged>(OnUIStateChanged);
        }

        private void OnPauseRequested()
        {
            _eventBus.Publish(new UIStateChanged(UIStates.InPausePanel));
            _controller.StopTime();
            _view.ShowPanel();
        }

        private void OnContinueRequested()
        {
            _eventBus.Publish(new UIStateChanged(UIStates.InActiveGameplay));
            _controller.StartTime();
            _view.HidePanel();
        }

        private void OnUIStateChanged(UIStateChanged state)
        {
            if (state.CurrentState == UIStates.InResultPanel)
            {
                _view.HidePanel();
                _view.HidePauseButton();
            }
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