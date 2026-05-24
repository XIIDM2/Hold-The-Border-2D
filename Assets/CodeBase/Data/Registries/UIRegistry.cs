using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data
{
    [CreateAssetMenu(fileName = "UIRegistry", menuName = "ScriptableObjects/Registries/UIRegistry")]
    public class UIRegistry : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject _levelButtonReference;
        [SerializeField] private AssetReferenceGameObject _skillButtonReference;
        [SerializeField] private AssetReferenceGameObject _damagePopupReference;
        [SerializeField] private AssetReferenceGameObject _towerPanelReference;

        public AssetReferenceGameObject LevelButtonReference => _levelButtonReference;
        public AssetReferenceGameObject SkillButtonReference => _skillButtonReference;
        public AssetReferenceGameObject DamagePopupReference => _damagePopupReference;
        public AssetReferenceGameObject TowerPanelReference => _towerPanelReference;
    }
}