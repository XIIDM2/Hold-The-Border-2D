using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Factories;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Services
{
    public class WaveControllerService : IWaveControllerService
    {
        public event UnityAction<int> NextWaveStarted;
        public int CurrentWaveIndex { get; private set; } = 1;
        public int WavesLength => _waves.WavesConfigs.Length;

        private Vector2 _spawnPosition;
        private IUnitFactory _unitFactory;
        private IPathProvider _pathProvider;

        private WaveData _waves;

        public WaveControllerService(IUnitFactory unitFactory, IPathProvider pathProvider, WaveData waves)
        {
            _unitFactory = unitFactory;
            _pathProvider = pathProvider;
            _waves = waves;
        }   

        public void Init(Vector2 spawnPosition)
        {
            _spawnPosition = spawnPosition;
        }

        public async UniTask WavesLogicAsync(CancellationToken cancellationToken)
        {
           await UniTask.Delay(TimeSpan.FromSeconds(_waves.WavesStartTimer), cancellationToken: cancellationToken);

            foreach (WaveData.WaveConfig wave in _waves.WavesConfigs)
            {
                NextWaveStarted?.Invoke(CurrentWaveIndex);

                foreach (WaveData.WaveConfig.WaveUnitsConfig units in wave.UnitsConfigs)
                {
                    for (int i = 0; i < units.Amount; i++)
                    {
                        await _unitFactory.CreateUnit(units.Type, _pathProvider.GetWaypoint(units.Path), _spawnPosition);
                        await UniTask.Delay(TimeSpan.FromSeconds(units.IntervalCurrent), cancellationToken : cancellationToken);
                    }

                    await UniTask.Delay(TimeSpan.FromSeconds(units.IntervalNext), cancellationToken: cancellationToken);
                }

                CurrentWaveIndex++;

                await UniTask.Delay(TimeSpan.FromSeconds(wave.WaveInterval), cancellationToken: cancellationToken);
            }

            Debug.Log("All Waves Finished");
        }
    }
}