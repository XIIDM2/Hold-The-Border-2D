using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    [CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Objects/Units/Unit Data")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject _prefabReference;
        [SerializeField] private AnimationData _animations;

        public AssetReferenceGameObject PrefabReference => _prefabReference;
        public AnimationData Animations => _animations;
    }
}