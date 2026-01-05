using Gameplay.Towers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    [CreateAssetMenu(fileName = "Tower Data", menuName = "Scriptable Objects/Towers/Tower Data")]
    public class TowerData : ScriptableObject
    {
        [System.Serializable]
        public class TowerTierConfig
        {
            public int Damage => _damage;
            public float AttackCooldown => _attackCooldown;
            public float AttackRadius => _attackRadius;

            public AnimationClip UpgradeAnimation => _upgradeAnimation;
            public AnimationClip IdleAnimation => _idleAnimation;
            public GameObject UnitPrefab => _unitPrefab;
            public Vector2[] UnitsPositions => _unitsPositions;

            [SerializeField] private int _damage;
            [SerializeField] private float _attackCooldown;
            [SerializeField] private float _attackRadius;

            [SerializeField] private AnimationClip _upgradeAnimation;
            [SerializeField] private AnimationClip _idleAnimation;

            [SerializeField] private GameObject _unitPrefab;
            [SerializeField] private Vector2[] _unitsPositions;
        }

        [SerializeField] private TowerType _type;
        [SerializeField] private AssetReference _towerPrefab;

        [SerializeField] private TowerTierConfig[] _tiersConfigs;

        public TowerType Type => _type;
        public AssetReference TowerPrefab => _towerPrefab;
        public TowerTierConfig[] TierConfigs => _tiersConfigs;
    }
}