using UnityEngine;

namespace Data
{
    public abstract class AttackSkillData : SkillData
    {
        [SerializeField] protected int _damage;
        public int Damage => _damage;
    }
}