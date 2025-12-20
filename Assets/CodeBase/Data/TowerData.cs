using UnityEngine;

[CreateAssetMenu(fileName = "Tower Data", menuName = "Scriptable Objects/Towers/Tower Data")]
public class TowerData : ScriptableObject
{
    [System.Serializable]
    public class TowerTierConfig
    {
        public int Damage => _damage;
        public float AttackCooldown => _attackCooldown;
        public float AttackRadius => _attackRadius;

        public GameObject VisualModel => _visualModel; 
        public AnimationClip UpgradeAnimation => _upgradeAnimation; 
        public AnimationClip IdleAnimation => _idleAnimation;

        [SerializeField] private int _damage;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _attackRadius;

        [SerializeField] private GameObject _visualModel;
        [SerializeField] private AnimationClip _upgradeAnimation;
        [SerializeField] private AnimationClip _idleAnimation;
    }

    [SerializeField] private TowerType _type;
    [SerializeField] private TowerTierConfig[] _tiersConfigs;

    public TowerType Type => _type;
    public TowerTierConfig[] TierConfigs => _tiersConfigs;
}
