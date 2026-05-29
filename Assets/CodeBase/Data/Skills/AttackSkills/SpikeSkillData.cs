using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    [CreateAssetMenu(fileName = "Spikes", menuName = "ScriptableObjects/Skills/AttackSkills/Spikes")]
    public class SpikeSkillData : AttackSkillData
    {
        [SerializeField] private AssetReferenceGameObject _prefab;
        [SerializeField] private float _duration;
        [SerializeField, Range(0, 100)] private int _slowPercentage;

        public AssetReferenceGameObject Prefab => _prefab;
        public float Duration => _duration;
        public float SlowPercentage => _slowPercentage;

        public override async UniTask Execute(Vector2? position = null)
        {
            Vector2 pos = position ?? Vector2.zero;

            SpikesSkill spikes = await _skillFactory.CreateSkillGameObject<SpikesSkill>(_prefab, pos);
            spikes.Init(_damage, _slowPercentage, _duration, _radius);
        }
    }
}