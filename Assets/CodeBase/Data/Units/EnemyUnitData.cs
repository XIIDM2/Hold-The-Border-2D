using Gameplay.Units.Enemy;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Enemy Unit Data", menuName = "ScriptableObjects/Units/Enemy Units/Unit Data")]
    public class EnemyUnitData : UnitData
    {
        [SerializeField] private EnemyUnitType _type;
        [SerializeField] private int _goldOnDeath;

        [SerializeField] private int _maxHealth;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _attackDamage;
        [SerializeField] private int _pathEndDamage;
        [SerializeField] private float _attackCooldown;

        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private AudioClip _hitSound;

        public EnemyUnitType Type => _type;
        public int GoldOnDeath => _goldOnDeath;
        public int MaxHealth => _maxHealth;
        public float MovementSpeed => _movementSpeed;
        public int AttackDamage => _attackDamage;
        public int PathEndDamage => _pathEndDamage;
        public float AttackCooldown => _attackCooldown;
        public AudioClip DeathSound => _deathSound;
        public AudioClip HitSound => _hitSound;
    }
}