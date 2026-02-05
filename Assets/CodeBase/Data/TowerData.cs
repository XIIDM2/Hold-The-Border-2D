using Gameplay.Towers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    [CreateAssetMenu(fileName = "Tower Data", menuName = "Scriptable Objects/Towers/Tower Data")]
    public class TowerData : ScriptableObject
    {
        [System.Serializable]
        public class TowerTiersConfig
        {
            public int UpgradePrice => _upgradePrice;
            public int SellPrice => _sellPrice;
            public int Damage => _damage;
            public float AttackCooldown => _attackCooldown;
            public float AttackRadius => _attackRadius;
            public GameObject AtackersModulePrefab => _attackersModulePrefab;
            public GameObject ProjectilePrefab => _projectilePrefab;
            public AnimationClip UpgradeAnimation => _upgradeAnimation;
            public AnimationClip IdleAnimation => _idleAnimation;


            [SerializeField] private int _upgradePrice;
            [SerializeField] private int _sellPrice;

            [SerializeField] private int _damage;
            [SerializeField] private float _attackCooldown;
            [SerializeField] private float _attackRadius;

            [SerializeField] private GameObject _attackersModulePrefab;
            [SerializeField] private GameObject _projectilePrefab;
            [SerializeField] private AnimationClip _upgradeAnimation;
            [SerializeField] private AnimationClip _idleAnimation;

        }

        [Header("Tower Info")]
        [SerializeField] private Sprite _icon;
        [SerializeField] private TowerType _type;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _buildPrice;

        [SerializeField] private AssetReferenceGameObject _prefabReference;

        [SerializeField] private TowerTiersConfig[] _tiersConfigs;

        public Sprite Icon => _icon;
        public TowerType Type => _type;
        public string Name => _name;
        public string Description => _description;
        public int BuildPrice => _buildPrice;
        public AssetReferenceGameObject TowerPrefabReference => _prefabReference;
        public TowerTiersConfig[] TierConfigs => _tiersConfigs;
    }
}