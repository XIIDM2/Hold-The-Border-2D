using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Services;
using System.Threading;
using UnityEngine;
using VContainer;

namespace Infrastructure.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private Transform _unitSpawnPoint;
        [SerializeField] private Transform[] _buildsitePoints;

        [Header("Dependencies")]
        private IWaveControllerService _waveService;
        private ITowerFactory _towerFactory;

        private LevelsLabels _levelLabel;

        [Inject]
        public void Construct(IWaveControllerService waveService, ITowerFactory towerFactory, LevelsLabels levelLabel)
        {
            _waveService = waveService;
            _towerFactory = towerFactory;
            _levelLabel = levelLabel;
        }

        public async void Awake()
        {
            _waveService.Init(_unitSpawnPoint.position);

            foreach (Transform point in _buildsitePoints)
            {
                await _towerFactory.CreateBuildSite(point.position, this.GetCancellationTokenOnDestroy());
            }

            _waveService.WavesLogicAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
}