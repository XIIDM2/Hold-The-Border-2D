using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Services;
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
        [SerializeField] protected float _cooldown;
        [SerializeField] protected SkillCastType _castType;

        protected IEventBus _eventBus;
        protected ISkillFactory _skillFactory;

        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public int Price => _price;
        public float Cooldown => _cooldown;
        public SkillCastType CastType => _castType;

        public void Init(IEventBus eventBus, ISkillFactory skillFactory)
        {
            _eventBus = eventBus;
            _skillFactory = skillFactory;
        }

        public abstract UniTask Execute();

    }
}