using Gameplay.Units.Enemy;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Enemy Unit Data", menuName = "Scriptable Objects/Units/Enemy Units/Unit Data")]
    public class EnemyUnitData : UnitData
    {
        [SerializeField] private EnemyUnitType _type;

        [SerializeField] private int _maxHealth;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _attackDamage;
        [SerializeField] private int _pathEndDamage;
        [SerializeField] private float _attackCooldown;

        public EnemyUnitType Type => _type;
        public int MaxHealth => _maxHealth;
        public float MovementSpeed => _movementSpeed;
        public int AttackDamage => _attackDamage;
        public int PathEndDamage => _pathEndDamage;
        public float AttackCooldown => _attackCooldown;
    }
}