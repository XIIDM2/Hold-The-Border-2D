using Data;
using Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace Infrastructure.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private Transform _unitSpawnPoint;

        [Header("Services")]
        private IPlayerController _playerController;
        private IPathProvider _pathService;
        private IWaveService _waveService;

        [Header("DATA")]
        private PlayerData _playerData;
        private WaveData _waveData;

        [Inject]
        public void Construct(IPlayerController playerController, IPathProvider pathService, IWaveService waveService, PlayerData playerData, WaveData waveData)
        {
            _playerController = playerController;
            _pathService = pathService;
            _waveService = waveService;

            _playerData = playerData;
            _waveData = waveData;

        }

        private async void Start()
        {
            _playerController.Init(_playerData);
            _waveService.Init(_unitSpawnPoint.position);

            await _waveService.WavesLogicAsync(_waveData, _pathService);

        }

        private void Update()
        {
            Debug.Log("Current Player Health: " + _playerController.Health.CurrentHealth);
        }

    }
}