using Cysharp.Threading.Tasks;
using Gameplay.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    [CreateAssetMenu(fileName = "ExplosiveBarrel", menuName = "ScriptableObjects/Skills/AttackSkills/ExplosiveBarrel")]
    public class ExplosiveBarrelSkillData : AttackSkillData
    {
        [SerializeField] private AssetReferenceGameObject _prefab;
        [SerializeField] private float _fuseDuration;

        public AssetReferenceGameObject Prefab => _prefab;
        public float FuseDuration => _fuseDuration;

        public override async UniTask Execute(Vector2? position = null)
        {
            Vector2 pos = position ?? Vector2.zero;

            ExplosiveBarrelSkill barrel = await _skillFactory.CreateSkillGameObject<ExplosiveBarrelSkill>(_prefab, pos);
            barrel.Init(_damage, _fuseDuration, _radius, _animationOverrideData);
        }
    }
}