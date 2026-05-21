using UnityEngine;

namespace Data
{
    public abstract class AttackSkillData : SkillData
    {
        [SerializeField] protected int _damage;
        [SerializeField] protected float _radius;

        public int Damage => _damage;
        public float Radius => _radius;
    }
}