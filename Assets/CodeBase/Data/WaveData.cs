using Gameplay.Path;
using Gameplay.Units.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Wave Data", menuName = "Scriptable Objects/Waves/Wave Data")]
    public class WaveData : ScriptableObject
    {
        [System.Serializable]
        public class WaveConfig
        {
            public IReadOnlyList<WaveUnitsConfig> UnitsConfigs => _unitsConfigs;
            public float WaveInterval => _waveInterval;

            [SerializeField] private WaveUnitsConfig[] _unitsConfigs;

            [SerializeField] private float _waveInterval;

            [System.Serializable]
            public struct WaveUnitsConfig
            {
                public EnemyUnitType Type => _type;
                public int Amount => _amount;
                public float IntervalCurrent => _intervalCurrent;
                public float IntervalNext => _intervalNext;

                public PathType Path => _path;

                [SerializeField] private EnemyUnitType _type;
                [SerializeField] private int _amount;

                [SerializeField] private float _intervalCurrent;
                [SerializeField] private float _intervalNext;

                [SerializeField] private PathType _path;

            }
        }

        [SerializeField] private float _wavesStartTimer;
        [SerializeField] private WaveConfig[] _wavesConfigs;

        public float WavesStartTimer => _wavesStartTimer;
        public WaveConfig[] WavesConfigs => _wavesConfigs;
    }
}