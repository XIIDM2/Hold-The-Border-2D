using Core.Interfaces;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace Infrastructure.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private Transform _unitSpawnPoint;
        [SerializeField] private Transform[] _buildsitePoints;

        [Header("Services")]
        private IWaveControllerService _waveService;
        private ITowerFactory _towerFactory;


        [Inject]
        public void Construct(IWaveControllerService waveService, ITowerFactory towerFactory)
        {
            _waveService = waveService;
            _towerFactory = towerFactory;
        }

        private void Awake()
        {
            _waveService.Initialize(_unitSpawnPoint.position);

            foreach (Transform point in _buildsitePoints)
            {
                _towerFactory.CreateBuildSite(point.position).Forget();
            }

            StartWavesLogic().Forget();
        }

        private async UniTaskVoid StartWavesLogic()
        {
            await _waveService.WavesLogicAsync(this.GetCancellationTokenOnDestroy());
        }
    }
}