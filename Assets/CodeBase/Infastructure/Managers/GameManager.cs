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
        private IPathProvider _pathService;
        private IWaveService _waveService;


        [Inject]
        public void Construct(IPathProvider pathService, IWaveService waveService, WaveData waveData)
        {
            _pathService = pathService;
            _waveService = waveService;
        }

        private void Awake()
        {
            _waveService.Init(_unitSpawnPoint.position);
        }

        private async void Start()
        {
            await _waveService.WavesLogicAsync(_pathService);
        }
    }
}