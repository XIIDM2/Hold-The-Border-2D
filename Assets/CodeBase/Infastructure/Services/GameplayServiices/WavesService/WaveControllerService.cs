using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Path;
using Gameplay.Units.Enemy;
using Infrastructure.Factories;
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
        public event UnityAction<float> NextWaveTimerTicked;
        public event UnityAction WaveFinished;
        public event UnityAction WavesCleared;

        public bool IsLastWave => CurrentWaveIndex >= WavesLength;
        public int CurrentWaveIndex { get; private set; } = 1;
        public int WavesLength => _wavesData.WavesConfigs.Length;

        public float TimerForNextWave {  get; private set; }

        private const int TIMER_TICK = 1;

        private Vector2 _spawnPosition;

        private readonly IUnitFactory _unitFactory;
        private readonly IPathProvider _pathProvider;
        private readonly IAudioService _audioService;
        private readonly WaveData _wavesData;

        private int _unitsAmount;
        private AudioClip _nextWaveStartedSound;

        private CancellationTokenSource _skipWaveTimerTokenSource;
        private CancellationToken _levelCtc;


        public WaveControllerService(IUnitFactory unitFactory, IPathProvider pathProvider, IAudioService audioService, WaveData wavesData, GameplayRegistry gameplayRegistry, CancellationToken levelCtc)
        {
            _unitFactory = unitFactory;
            _pathProvider = pathProvider;
            _audioService = audioService;
            _wavesData = wavesData;

            _unitsAmount = _wavesData.WaveUnitsAmount;
            _nextWaveStartedSound = gameplayRegistry.SFXRegistry.StartWaveSound;
            _levelCtc = levelCtc;
        }

        public void Start()
        {
            _unitFactory.UnitCreated += OnUnitCreated;
            NextWaveStarted += OnNextWaveStarted;
        }

        public void Dispose()
        {
            _unitFactory.UnitCreated -= OnUnitCreated;
            NextWaveStarted -= OnNextWaveStarted;
        }

        public void Init(Vector2 spawnPosition)
        {
            _spawnPosition = spawnPosition;
        }

        public async UniTask InitUnitsPools()
        {
            foreach (EnemyUnitType type in _wavesData.LevelUnitTypes)
            {
                await _unitFactory.InitPool(type, _levelCtc);
            }
        }

        public async UniTask WavesLogicAsync()
        {
            foreach (var wave in _wavesData.WavesConfigs)
            {
                NextWaveStarted?.Invoke(CurrentWaveIndex);

                foreach (var units in wave.UnitsConfigs)
                {
                    for (int i = 0; i < units.Amount; i++)
                    {
                        await _unitFactory.CreateUnit(units.Type, _pathProvider.GetWaypoint(units.Path), _spawnPosition, _levelCtc);

                        await UniTask.WaitForSeconds(units.IntervalCurrent, cancellationToken : _levelCtc);
                    }

                    await UniTask.WaitForSeconds(units.IntervalNext, cancellationToken: _levelCtc);
                }

                WaveFinished?.Invoke();

                CurrentWaveIndex++;

                _skipWaveTimerTokenSource = new CancellationTokenSource();

                TimerForNextWave = wave.WaveInterval;

                using (CancellationTokenSource linkedCtc = CancellationTokenSource.CreateLinkedTokenSource(_levelCtc, _skipWaveTimerTokenSource.Token))
                {
                    while (TimerForNextWave > 0)
                    {
                        NextWaveTimerTicked?.Invoke(TimerForNextWave);

                        bool isCancelled = await UniTask.WaitForSeconds(TIMER_TICK, cancellationToken: linkedCtc.Token).SuppressCancellationThrow();

                        if (isCancelled)
                        {
                            if (_levelCtc.IsCancellationRequested) return;
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

        public void OnNextWaveStarted(int _)
        {
            _audioService.PlaySound(_nextWaveStartedSound);
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