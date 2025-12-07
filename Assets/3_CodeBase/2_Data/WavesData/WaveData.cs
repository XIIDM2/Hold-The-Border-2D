using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Data", menuName = "Scriptable Objects/Waves/Wave Data")]
public class WaveData : ScriptableObject
{
    [System.Serializable]
    public class Wave
    {
        public IReadOnlyList<WaveUnits> Units => _units;
        public float WaveInterval => _waveInterval;

        [SerializeField] private WaveUnits[] _units;

        [Header("Interval Between Next Wave")]
        [SerializeField] private float _waveInterval;

        [System.Serializable]
        public struct WaveUnits
        {
            public UnitType Type => _unitType;
            public int Amount => _amount;
            public float Interval => _interval;

            public PathType Path => _path;

            [SerializeField] private UnitType _unitType;
            [SerializeField] private int _amount;

            [Header("Interval Between Units")]
            [SerializeField] private float _interval;

            [Header("Start Waypoint")]
            [SerializeField] private PathType _path;

        }
    }

    [SerializeField] private float _wavesStartInterval;
    [SerializeField] private Wave[] _waves;

    public float WavesStartInterval => _wavesStartInterval;
    public Wave[] Waves => _waves;
}
