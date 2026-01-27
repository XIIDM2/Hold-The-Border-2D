using Cysharp.Threading.Tasks;
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
        private IWaveService _waveService;


        [Inject]
        public void Construct(IWaveService waveService, WaveData waveData)
        {
            _waveService = waveService;
        }

        private void Awake()
        {
            _waveService.Init(_unitSpawnPoint.position);
        }

        private async void Start()
        {
            await _waveService.WavesLogicAsync(this.GetCancellationTokenOnDestroy());
        }
    }
}