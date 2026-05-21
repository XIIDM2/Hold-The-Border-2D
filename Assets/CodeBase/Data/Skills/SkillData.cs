using UnityEngine;
using UnityEngine.UI;

namespace Data
{
    public abstract class SkillData : ScriptableObject
    {
        [SerializeField] protected string _name;
        [SerializeField] protected string _description;
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected int _price;
        [SerializeField] protected SkillCastType _castType;

        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public int Price => _price;
        public SkillCastType CastType => _castType;

    }
}