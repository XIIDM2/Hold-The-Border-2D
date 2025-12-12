using System.Collections;
using UnityEngine;

public interface IWaveService
{
    IEnumerator WavesLogicRoutine(WaveData data, IPathProvider pathProvider);
}
