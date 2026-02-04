using Cysharp.Threading.Tasks;
using Data;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;


public interface IWaveControllerService
{
    public int CurrentWaveIndex { get; }
    public int WavesLength { get; }

    public event UnityAction<int> NextWaveStarted;
    void Init(Vector2 spawnPosition);
    UniTask WavesLogicAsync(CancellationToken cancellationToken);
}
