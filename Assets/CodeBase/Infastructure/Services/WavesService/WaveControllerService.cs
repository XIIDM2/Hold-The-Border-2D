using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Path;
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
        public int WavesLength => _wavesData.WavesConfigs.Length;

        private Vector2 _spawnPosition;
        private IUnitFactory _unitFactory;
        private IPathProvider _pathProvider;

        private WaveData _wavesData;

        public WaveControllerService(IUnitFactory unitFactory, IPathProvider pathProvider, WaveData wavesData)
        {
            _unitFactory = unitFactory;
            _pathProvider = pathProvider;
            _wavesData = wavesData;
        }   

        public void Init(Vector2 spawnPosition)
        {
            _spawnPosition = spawnPosition;
        }

        public async UniTask WavesLogicAsync(CancellationToken cancellationToken)
        {
           await UniTask.Delay(TimeSpan.FromSeconds(_wavesData.WavesStartTimer), cancellationToken: cancellationToken);

            foreach (WaveData.WaveConfig wave in _wavesData.WavesConfigs)
            {
                NextWaveStarted?.Invoke(CurrentWaveIndex);

                foreach (WaveData.WaveConfig.WaveUnitsConfig units in wave.UnitsConfigs)
                {
                    for (int i = 0; i < units.Amount; i++)
                    {
                        await _unitFactory.CreateUnit(units.Type, _pathProvider.GetWaypoint(units.Path), _spawnPosition, cancellationToken);
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