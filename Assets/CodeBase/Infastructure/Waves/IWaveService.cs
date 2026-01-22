using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;


public interface IWaveService
{
    void Init(Vector2 spawnPosition);
    UniTask WavesLogicAsync(WaveData data, IPathProvider pathProvider);
}
