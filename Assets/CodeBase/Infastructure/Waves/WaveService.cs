using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Factories;
using System;
using UnityEngine;

namespace Infrastructure.Services
{
    public class WaveService : IWaveService
    {
        private Vector2 _spawnPosition;
        private IUnitFactory _unitFactory;
        public WaveService(IUnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public void Init(Vector2 spawnPosition)
        {
            _spawnPosition = spawnPosition;
        }

        public async UniTask WavesLogicAsync(WaveData data, IPathProvider pathProvider)
        {
           await UniTask.Delay(TimeSpan.FromSeconds(data.WavesStartTimer));

            foreach (WaveData.WaveConfig wave in data.Waves)
            {
                foreach (WaveData.WaveConfig.WaveUnitsConfig units in wave.Units)
                {
                    for (int i = 0; i < units.Amount; i++)
                    {
                        await _unitFactory.CreateUnit(units.Type, pathProvider.GetWaypoint(units.Path), _spawnPosition);
                        await UniTask.Delay(TimeSpan.FromSeconds(units.IntervalCurrent));
                    }

                    await UniTask.Delay(TimeSpan.FromSeconds(units.IntervalNext));
                }

                await UniTask.Delay(TimeSpan.FromSeconds(wave.WaveInterval));
            }

            Debug.Log("All Waves Finished");
        }
    }
}