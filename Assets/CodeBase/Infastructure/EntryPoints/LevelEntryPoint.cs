using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Services;
using System.Threading;
using UnityEngine;
using VContainer;

namespace Infrastructure.Managers
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private Transform _unitSpawnPoint;
        [SerializeField] private Transform[] _buildsitePoints;

        [Header("Dependencies")]
        private IWaveControllerService _waveService;
        private IAssetProviderService _assetProviderService;
        private ITowerFactory _towerFactory;

        private LevelsLabels _levelLabel;

        [Inject]
        public void Construct(IWaveControllerService waveService, IAssetProviderService assetProviderService, ITowerFactory towerFactory, LevelsLabels levelLabel)
        {
            _waveService = waveService;
            _assetProviderService = assetProviderService;
            _towerFactory = towerFactory;
            _levelLabel = levelLabel;
        }

        public async void Awake()
        {
            await _assetProviderService.LoadMultipleAssetsByLabel(_levelLabel.ToString(), this.GetCancellationTokenOnDestroy());

            _waveService.Init(_unitSpawnPoint.position);

            foreach (Transform point in _buildsitePoints)
            {
                await _towerFactory.CreateBuildSite(point.position, this.GetCancellationTokenOnDestroy());
            }

            _waveService.WavesLogicAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
}