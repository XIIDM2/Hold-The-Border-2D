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
using static UnityEngine.EventSystems.EventTrigger;

namespace Infrastructure.Services
{
    public class WaveControllerService : IWaveControllerService, IStartable, IDisposable
    {
        public event UnityAction<int> NextWaveStarted;
        public event UnityAction<float> NextWaveTimerTicked;
        public event UnityAction WaveFinished;
        public event UnityAction WavesCleared;

        public bool IsLastWave => CurrentWaveIndex >= WavesLength;
        public int CurrentWaveIndex { get; private set; } = 1;
        public int WavesLength => _wavesData.WavesConfigs.Length;

        public float TimerForNextWave {  get; private set; }

        private Vector2 _spawnPosition;

        private readonly IUnitFactory _unitFactory;
        private readonly IPathProvider _pathProvider;
        private readonly WaveData _wavesData;

        private int _unitsAmount;

        private CancellationTokenSource _skipWaveTimerTokenSource;

        public WaveControllerService(IUnitFactory unitFactory, IPathProvider pathProvider, WaveData wavesData)
        {
            _unitFactory = unitFactory;
            _pathProvider = pathProvider;
            _wavesData = wavesData;

            _unitsAmount = _wavesData.WaveUnitsAmount;
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

                WaveFinished?.Invoke();

                CurrentWaveIndex++;

                _skipWaveTimerTokenSource = new CancellationTokenSource();

                TimerForNextWave = wave.WaveInterval;

                using (CancellationTokenSource linkedCtc = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _skipWaveTimerTokenSource.Token))
                {
                    while (TimerForNextWave > 0)
                    {
                        NextWaveTimerTicked?.Invoke(TimerForNextWave);

                        bool isCancelled = await UniTask.Delay(1000, cancellationToken: linkedCtc.Token).SuppressCancellationThrow();

                        if (isCancelled)
                        {
                            if (cancellationToken.IsCancellationRequested) return;
                            break;
                        }

                        TimerForNextWave--;
                    }
                }


                _skipWaveTimerTokenSource.Dispose();
                _skipWaveTimerTokenSource = null;


            }

            Debug.Log("All Waves Finished");
        }

        public void SkipWaveTimer()
        {
            _skipWaveTimerTokenSource?.Cancel();
        }

        private void OnUnitCreated(EnemyUnitController enemy)
        {
            enemy.Removed += OnUnitRemove;
        }

        private void OnUnitRemove(EnemyUnitController enemy)
        {
            enemy.Removed -= OnUnitRemove;

            _unitsAmount--;

            if (_unitsAmount == 0) WavesCleared?.Invoke();
        }

    }
}