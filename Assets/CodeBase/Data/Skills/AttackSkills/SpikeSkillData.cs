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
    }
}