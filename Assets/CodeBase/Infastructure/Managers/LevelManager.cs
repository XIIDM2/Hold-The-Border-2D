using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace Infrastructure.Managers
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        [Header("Level Settings")]
        [SerializeField] private Transform _unitSpawnPoint;
        [SerializeField] private Transform[] _buildsitePoints;

        [Header("Dependencies")]
        private IWaveControllerService _waveService;
        private ITowerFactory _towerFactory;

        public GameObject GameObject => this.gameObject;

        [Inject]
        public void Construct(IWaveControllerService waveService, ITowerFactory towerFactory)
        {
            _waveService = waveService;
            _towerFactory = towerFactory;
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
    }
}