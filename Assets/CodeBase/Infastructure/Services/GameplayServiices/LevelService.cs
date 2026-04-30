using Cysharp.Threading.Tasks;
using Infrastructure.Events;
using System;
using UnityEngine.Events;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Services
{
    public class LevelService : ILevelService, IDisposable
    {
        private IWaveControllerService _waveService;
        private IEventBus _eventBus;
        private SceneController _sceneController;

        public event UnityAction Victory;
        public event UnityAction Defeat;

        [Inject]
        public void Construct(IWaveControllerService waveService, IEventBus eventBus, SceneController sceneController)
        {
            _waveService = waveService;
            _eventBus = eventBus;
            _sceneController = sceneController;
        }

        public void Init()
        {
            _eventBus.Subscribe<PlayerDiedEvent>(OnPlayerDeath);
            _waveService.WavesCleared += OnWaveFinished;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<PlayerDiedEvent>(OnPlayerDeath);
            _waveService.WavesCleared -= OnWaveFinished;
        }

        public void StartWaves()
        {
            _waveService.WavesLogicAsync().Forget();
        }

        private void OnPlayerDeath(PlayerDiedEvent _)
        {
            _sceneController.StopTime();
            Defeat?.Invoke();
        }

        private void OnWaveFinished()
        {
            _sceneController.StopTime();
            Victory?.Invoke();
        }
    }
}