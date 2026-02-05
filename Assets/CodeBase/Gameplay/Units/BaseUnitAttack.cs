using Core.Utilities.CustomProperties;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Gameplay.Units
{
    public abstract class BaseUnitAttack : MonoBehaviour
    {
        public int Damage => _damage;
        public float Cooldown => _cooldown;

        [SerializeField, ReadOnly] protected int _damage;
        [SerializeField, ReadOnly] protected float _cooldown;

        public virtual void Initialize(int damage, float coolDown)
        {
            _damage = damage;
            _cooldown = coolDown;
        }

        public abstract void Attack(ITargetable target);
    }
}