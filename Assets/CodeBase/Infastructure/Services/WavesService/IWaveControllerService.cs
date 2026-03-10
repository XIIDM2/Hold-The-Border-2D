using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Services
{
    public interface IWaveControllerService
    {
        int CurrentWaveIndex { get; }
        int WavesLength { get; }

        event UnityAction<int> NextWaveStarted;
        event UnityAction WaveFinished;
        void Init(Vector2 spawnPosition);

        UniTask InitUnitsPools(CancellationToken cancellationToken);
        UniTask WavesLogicAsync(CancellationToken cancellationToken);
    }
}