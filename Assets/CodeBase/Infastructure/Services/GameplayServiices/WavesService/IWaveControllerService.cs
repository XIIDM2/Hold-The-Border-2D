using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Services
{
    public interface IWaveControllerService
    {
        bool IsLastWave {  get; }
        int CurrentWaveIndex { get; }
        int WavesLength { get; }

        float TimerForNextWave { get; }

        event UnityAction<int> NextWaveStarted;
        event UnityAction<float> NextWaveTimerTicked;
        event UnityAction WaveFinished;
        void Init(Vector2 spawnPosition);

        UniTask InitUnitsPools();
        UniTask WavesLogicAsync();
        void SkipWaveTimer();
    }
}