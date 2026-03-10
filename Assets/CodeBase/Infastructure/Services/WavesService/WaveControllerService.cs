using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Path;
using Gameplay.Units.Enemy;
using Infrastructure.Factories;
using Infrastructure.Interfaces;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using VContainer.Unity;

namespace Infrastructure.Services
{
    public class WaveControllerService : IWaveControllerService, IStartable, IDisposable
    {
        public event UnityAction<int> NextWaveStarted;
        public event UnityAction WaveFinished;
        public int CurrentWaveIndex { get; private set; } = 1;
        public int WavesLength => _wavesData.WavesConfigs.Length;

        private Vector2 _spawnPosition;

        private readonly IUnitFactory _unitFactory;
        private readonly IPathProvider _pathProvider;
        private readonly WaveData _wavesData;

        private int UnitsAmount;

        public WaveControllerService(IUnitFactory unitFactory, IPathProvider pathProvider, WaveData wavesData)
        {
            _unitFactory = unitFactory;
            _pathProvider = pathProvider;
            _wavesData = wavesData;

            UnitsAmount = _wavesData.WaveUnitsAmount;
        }

        public void Start()
        {
            _unitFactory.UnitCreated += OnUnitCreated;
        }

        public void Dispose()
        {
            _unitFactory.UnitCreated -= OnUnitCreated;
        }

        public void Init(Vector2 spawnPosition)
        {
            _spawnPosition = spawnPosition;
        }

        public async UniTask InitUnitsPools(CancellationToken cancellationToken)
        {
            foreach (EnemyUnitType type in _wavesData.LevelUnitTypes)
            {
                await _unitFactory.InitPool(type, cancellationToken);
            }
        }

        public async UniTask WavesLogicAsync(CancellationToken cancellationToken)
        {
           await UniTask.Delay(TimeSpan.FromSeconds(_wavesData.WavesStartTimer), cancellationToken: cancellationToken);

            foreach (var wave in _wavesData.WavesConfigs)
            {
                NextWaveStarted?.Invoke(CurrentWaveIndex);

                foreach (var units in wave.UnitsConfigs)
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

        private void OnUnitCreated(EnemyUnitController enemy)
        {
            enemy.Health.Death += OnUnitDeath;
        }

        private void OnUnitDeath(IDamageable damageable)
        {
            damageable.Death -= OnUnitDeath;

            UnitsAmount--;

            if (UnitsAmount == 0) WaveFinished?.Invoke();
        }

    }
}