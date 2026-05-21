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
    }
}