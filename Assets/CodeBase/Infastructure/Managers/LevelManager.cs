using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Player;
using Infrastructure.Events;
using Infrastructure.Factories;
using Infrastructure.Services;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Managers
{
    public class LevelManager : MonoBehaviour, ILevelManager, IAsyncStartable, IDisposable
    {
        [Header("Level Settings")]
        [SerializeField] private Transform _unitSpawnPoint;
        [SerializeField] private Transform[] _buildsitePoints;

        [Header("Dependencies")]
        private IWaveControllerService _waveService;
        private ITowerFactory _towerFactory;
        private IEventBus _eventBus;

        private SceneController _sceneController;
        private LevelData _data;

        public GameObject GameObject => this.gameObject;

        public event UnityAction Victory;
        public event UnityAction Defeat;

        [Inject]
        public void Construct(IWaveControllerService waveService, ITowerFactory towerFactory, IEventBus eventBus, SceneController sceneController, LevelData data)
        {
            _eventBus = eventBus;
            _waveService = waveService;
            _towerFactory = towerFactory;
            _sceneController = sceneController;
            _data = data;
        }

        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            _eventBus.Subscribe<PlayerDiedEvent>(OnPlayerDeath);
            _waveService.WavesCleared += OnWaveFinished;

            await InitLevel();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<PlayerDiedEvent>(OnPlayerDeath);
            _waveService.WavesCleared -= OnWaveFinished;
        }

        public void StartWaves()
        {
            _waveService.WavesLogicAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask InitLevel()
        {
            _waveService.Init(_unitSpawnPoint.position);

            await _waveService.InitUnitsPools(this.GetCancellationTokenOnDestroy());

            foreach (Transform point in _buildsitePoints)
            {
                await _towerFactory.CreateBuildSite(point.position, this.GetCancellationTokenOnDestroy());
            }

            _eventBus.Publish(new LevelStartedEvent(_data.Music, _data.AmbienceSound));

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