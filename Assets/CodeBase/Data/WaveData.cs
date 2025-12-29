using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Data", menuName = "Scriptable Objects/Waves/Wave Data")]
public class WaveData : ScriptableObject
{
    [System.Serializable]
    public class WaveConfig
    {
        public IReadOnlyList<WaveUnitsConfig> Units => _units;
        public float WaveInterval => _waveInterval;

        [SerializeField] private WaveUnitsConfig[] _units;

        [Header("Interval Between Next Wave")]
        [SerializeField] private float _waveInterval;

        [System.Serializable]
        public struct WaveUnitsConfig
        {
            public EnemyUnitType Type => _unitType;
            public int Amount => _amount;
            public float IntervalCurrent => _intervalCurrent;
            public float IntervalNext => _intervalNext;

            public PathType Path => _path;

            [SerializeField] private EnemyUnitType _unitType;
            [SerializeField] private int _amount;

            [Header("Interval Between Current Units")]
            [SerializeField] private float _intervalCurrent;

            [Header("Interval Between Next Units")]
            [SerializeField] private float _intervalNext;

            [Header("Start Waypoint")]
            [SerializeField] private PathType _path;

        }
    }

    [SerializeField] private float _wavesStartTimer;
    [SerializeField] private WaveConfig[] _waves;

    public float WavesStartTimer => _wavesStartTimer;
    public WaveConfig[] Waves => _waves;
}
