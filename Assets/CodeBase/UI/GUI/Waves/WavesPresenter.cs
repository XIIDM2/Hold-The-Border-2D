using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Infrastructure.Events;
using Infrastructure.Services;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class WavesPresenter : IStartable, IDisposable
    {
        private readonly WavesView _view;

        private readonly IWaveControllerService _service;

        private readonly IPlayerController _player;

        private readonly IEventBus _eventBus;

        private readonly ILevelService _manager;

        public WavesPresenter(WavesView view, IWaveControllerService service, IPlayerController player, IEventBus eventBus, ILevelService manager)
        {
            _view = view;
            _service = service;
            _player = player;
            _eventBus = eventBus;
            _manager = manager;
        }

        public void Start()
        {
            _view.Init(_service.CurrentWaveIndex, _service.WavesLength);

            _view.WavesStartRequested += OnWavesStartRequested;
            _view.WavesTimerSkipRequested += OnWaveTimerSkipRequested;

            _service.NextWaveStarted += OnNextWaveStarted;
            _service.NextWaveTimerTicked += OnNextWaveTimerTicked;
            _service.WaveFinished += OnWaveFinished;

            _eventBus.Subscribe<UIStateChanged>(OnUIStateChanged);
        }

        public void Dispose()
        {
            _view.WavesStartRequested -= OnWavesStartRequested;
            _view.WavesTimerSkipRequested -= OnWaveTimerSkipRequested;

            _service.NextWaveStarted -= OnNextWaveStarted;
            _service.NextWaveTimerTicked -= OnNextWaveTimerTicked;
            _service.WaveFinished -= OnWaveFinished;

            _eventBus.Unsubscribe<UIStateChanged>(OnUIStateChanged);
        }

        private void OnWavesStartRequested()
        {
            _service.WavesLogicAsync().Forget();
        }

        private void OnWaveTimerSkipRequested()
        {
            _service.SkipWaveTimer();
            _player.AddGold((int)_service.TimerForNextWave * _player.SkipWaveTimerGoldMultiplier);
        }

        private void OnNextWaveTimerTicked(float timeLeft)
        {
            _view.OnNextWaveTimerTicked(timeLeft);
        }

        private void OnWaveFinished()
        {
            if (_service.IsLastWave) return;

            _view.OnWaveFinished();
        }

        private void OnNextWaveStarted(int waveIndex)
        {
            _view.OnNextWaveStarted(waveIndex);
        }

        private void OnUIStateChanged(UIStateChanged state)
        {
            if (state.CurrentState == UIStates.InTowerBuildingPanel || state.CurrentState == UIStates.InPausePanel)
            {
                _view.HideButtonsPanel();
            }
            else if (state.CurrentState == UIStates.InActiveGameplay)
            {
                _view.ShowButtonsPanel();
            }
        }

    }
}