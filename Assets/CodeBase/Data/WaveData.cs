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

        public float WavesStartTimer => _wavesStartTimer;
        public WaveConfig[] WavesConfigs => _wavesConfigs;
        public int WaveUnitsAmount => _waveUnitsAmount;
        public List<EnemyUnitType> LevelUnitTypes  => _levelUnitTypes;

        private const int SLIME_SPLIT_UNIT_AMOUNT = 2;

        [SerializeField] private float _wavesStartTimer;
        [SerializeField] private WaveConfig[] _wavesConfigs;
        [SerializeField] private int _waveUnitsAmount;
        [SerializeField] private List<EnemyUnitType> _levelUnitTypes  = new List<EnemyUnitType>();

        private void OnValidate()
        {
            _levelUnitTypes.Clear();
            _waveUnitsAmount = 0;

            HashSet<EnemyUnitType> uniqueUnitTypes = new HashSet<EnemyUnitType>();

            foreach (var waveConfig in _wavesConfigs)
            {
                foreach (var unitsConfig in waveConfig.UnitsConfigs)
                {
                    if (uniqueUnitTypes.Add(unitsConfig.Type))
                    {
                        _levelUnitTypes.Add(unitsConfig.Type);
                    }

                    _waveUnitsAmount += unitsConfig.Amount;

                    if (unitsConfig.Type == EnemyUnitType.Slime)
                    {
                        _waveUnitsAmount += unitsConfig.Amount * SLIME_SPLIT_UNIT_AMOUNT;
                    }
                }
            }
        }

    }
}