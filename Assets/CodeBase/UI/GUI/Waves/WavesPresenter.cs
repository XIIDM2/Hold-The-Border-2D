using Gameplay.Player;
using Infrastructure.Services;
using System;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class WavesPresenter : IStartable, IDisposable
    {
        private readonly WavesView _view;

        private readonly IWaveControllerService _service;
        private IPlayerController _player;
        private readonly ILevelService _manager;

        public WavesPresenter(WavesView view, IWaveControllerService service, IPlayerController player, ILevelService manager)
        {
            _view = view;
            _service = service;
            _player = player;
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
        }

        public void Dispose()
        {
            _view.WavesStartRequested -= OnWavesStartRequested;
            _view.WavesTimerSkipRequested -= OnWaveTimerSkipRequested;

            _service.NextWaveStarted -= OnNextWaveStarted;
            _service.NextWaveTimerTicked -= OnNextWaveTimerTicked;
            _service.WaveFinished -= OnWaveFinished;
        }

        private void OnWavesStartRequested()
        {
            _manager.StartWaves();
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

    }
}