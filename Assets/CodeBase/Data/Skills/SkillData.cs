using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Services;
using UnityEngine;

namespace Data
{
    public abstract class SkillData : ScriptableObject
    {
        [SerializeField] protected string _name;
        [SerializeField, TextArea] protected string _description;
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected int _price;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected SkillCastType _castType;
        [SerializeField] protected float _radius;

        [SerializeField] protected AnimationOverrideData _animationOverrideData;

        protected IEventBus _eventBus;
        protected ISkillFactory _skillFactory;

        public string Name => _name;
        public Sprite Icon => _icon;
        public int Price => _price;
        public float Cooldown => _cooldown;
        public SkillCastType CastType => _castType;
        public float Radius => _radius;

        public void Init(IEventBus eventBus, ISkillFactory skillFactory)
        {
            _eventBus = eventBus;
            _skillFactory = skillFactory;
        }

        public virtual string GetDescription() => null;

        public abstract UniTask Execute(Vector2? position = null);

    }
}