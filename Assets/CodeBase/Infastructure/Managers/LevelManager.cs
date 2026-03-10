using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.Events;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Managers
{
    public class LevelManager : MonoBehaviour, ILevelManager, IStartable, IDisposable
    {
        [Header("Level Settings")]
        [SerializeField] private Transform _unitSpawnPoint;
        [SerializeField] private Transform[] _buildsitePoints;

        [Header("Dependencies")]
        private IPlayerController _player;
        private IWaveControllerService _waveService;
        private ITowerFactory _towerFactory;

        private SceneController _sceneController;

        public GameObject GameObject => this.gameObject;

        public event UnityAction Victory;
        public event UnityAction Defeat;

        [Inject]
        public void Construct(IPlayerController player, IWaveControllerService waveService, ITowerFactory towerFactory, SceneController sceneController)
        {
            _player = player;
            _waveService = waveService;
            _towerFactory = towerFactory;
            _sceneController = sceneController;
        }

        public async void Awake()
        {
            _waveService.Init(_unitSpawnPoint.position);

            await _waveService.InitUnitsPools(this.GetCancellationTokenOnDestroy());

            foreach (Transform point in _buildsitePoints)
            {
                await _towerFactory.CreateBuildSite(point.position, this.GetCancellationTokenOnDestroy());
            }

            _waveService.WavesLogicAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }


        void IStartable.Start()
        {
            _player.Health.Death += OnPlayerDeath;
            _waveService.WaveFinished += OnWaveFinished;
        }

        public void Dispose()
        {
            _player.Health.Death -= OnPlayerDeath;
            _waveService.WaveFinished -= OnWaveFinished;
        }

        private void OnPlayerDeath(IDamageable _)
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