using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using UnityEngine.Events;


public interface IWaveService
{
    public int CurrentWaveIndex { get; }
    public int WavesLength { get; }

    public event UnityAction<int> NextWaveStarted;
    void Init(Vector2 spawnPosition);
    UniTask WavesLogicAsync(IPathProvider pathProvider);
}
