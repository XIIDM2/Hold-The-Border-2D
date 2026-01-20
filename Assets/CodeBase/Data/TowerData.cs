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
            public GameObject TowerVisualPrefab => _towerVisualPrefab;
            public GameObject ProjectilePrefab => _projectilePrefab;

            public int UpgradePrice => _upgradePrice;
            public int SellPrice => _sellPrice;

            public int Damage => _damage;
            public float AttackCooldown => _attackCooldown;
            public float AttackRadius => _attackRadius;

            public AnimationClip UpgradeAnimation => _upgradeAnimation;
            public AnimationClip IdleAnimation => _idleAnimation;

            [SerializeField] private GameObject _towerVisualPrefab;
            [SerializeField] private GameObject _projectilePrefab;

            [SerializeField] private int _upgradePrice;
            [SerializeField] private int _sellPrice;

            [SerializeField] private int _damage;
            [SerializeField] private float _attackCooldown;
            [SerializeField] private float _attackRadius;

            [SerializeField] private AnimationClip _upgradeAnimation;
            [SerializeField] private AnimationClip _idleAnimation;

        }

        [SerializeField] private TowerType _type;
        [SerializeField] private int _buildPrice;

        [SerializeField] private AssetReferenceGameObject _prefabReference;

        [SerializeField] private TowerTierConfig[] _tiersConfigs;

        public TowerType Type => _type;
        public int BuildPrice => _buildPrice;
        public AssetReferenceGameObject TowerPrefabReference => _prefabReference;
        public TowerTierConfig[] TierConfigs => _tiersConfigs;
    }
}