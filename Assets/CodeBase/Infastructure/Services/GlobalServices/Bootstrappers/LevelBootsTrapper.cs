using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Level;
using Gameplay.Path;
using Gameplay.Player;
using Infrastructure.Events;
using Infrastructure.Factories;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace Infrastructure.Services.Bootstrappers
{
    public class LevelBootsTrapper : IAsyncStartable
    {
        private readonly IEventBus _eventBus;
        private readonly ILevelService _levelService;
        private readonly IPathProvider _pathProvider;
        private readonly IWaveControllerService _waveService;
        private readonly ITowerFactory _towerFactory;
        private readonly IPlayerController _playerController;

        private readonly LevelData _data;
        private readonly LevelConfig _config;

        private readonly CancellationToken _ctc;

        public LevelBootsTrapper(IEventBus eventBus, ILevelService levelService, IPathProvider pathProvider, IWaveControllerService waveService, ITowerFactory towerFactory, IPlayerController playerController, LevelData data, LevelConfig config, CancellationToken ctc)
        {
            _eventBus = eventBus;
            _levelService = levelService;
            _pathProvider = pathProvider;
            _waveService = waveService;
            _playerController = playerController;
            _towerFactory = towerFactory;

            _data = data;
            _config = config;
            _ctc = ctc;
        }

        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            _eventBus.Publish(new LevelStartedEvent(_data.Music, _data.AmbienceSound));

            _pathProvider.Init();

            _waveService.Init(_config.UnitSpawnPoint.position);

            await _waveService.InitUnitsPools();

            foreach (Transform point in _config.BuildsitePoints)
            {
                await _towerFactory.CreateBuildSite(point.position, _ctc);
            }

            _levelService.Init();

            _playerController.Init();
        }
    }
}