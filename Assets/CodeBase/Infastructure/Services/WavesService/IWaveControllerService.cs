using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Services
{
    public interface IWaveControllerService
    {
        public int CurrentWaveIndex { get; }
        public int WavesLength { get; }

        public event UnityAction<int> NextWaveStarted;
        void Initialize(Vector2 spawnPosition);
        UniTask WavesLogicAsync(CancellationToken cancellationToken);
    }
}