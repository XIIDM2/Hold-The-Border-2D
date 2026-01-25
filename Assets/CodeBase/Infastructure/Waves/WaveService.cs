using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Factories;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Services
{
    public class WaveService : IWaveService
    {
        public event UnityAction<int> NextWaveStarted;
        public int CurrentWaveIndex { get; private set; } = 1;
        public int WavesLength => _waves.Waves.Length;

        private Vector2 _spawnPosition;
        private IUnitFactory _unitFactory;

        private WaveData _waves;

        public WaveService(IUnitFactory unitFactory, WaveData waves)
        {
            _unitFactory = unitFactory;
            _waves = waves;
        }

        public void Init(Vector2 spawnPosition)
        {
            _spawnPosition = spawnPosition;
        }

        public async UniTask WavesLogicAsync(IPathProvider pathProvider)
        {
           await UniTask.Delay(TimeSpan.FromSeconds(_waves.WavesStartTimer));

            foreach (WaveData.WaveConfig wave in _waves.Waves)
            {
                NextWaveStarted?.Invoke(CurrentWaveIndex);

                foreach (WaveData.WaveConfig.WaveUnitsConfig units in wave.Units)
                {
                    for (int i = 0; i < units.Amount; i++)
                    {
                        await _unitFactory.CreateUnit(units.Type, pathProvider.GetWaypoint(units.Path), _spawnPosition);
                        await UniTask.Delay(TimeSpan.FromSeconds(units.IntervalCurrent));
                    }

                    await UniTask.Delay(TimeSpan.FromSeconds(units.IntervalNext));
                }

                CurrentWaveIndex++;

                await UniTask.Delay(TimeSpan.FromSeconds(wave.WaveInterval));
            }

            Debug.Log("All Waves Finished");
        }
    }
}