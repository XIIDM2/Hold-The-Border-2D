using Core.Utilities.Extentions;
using Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Utilities.CustomInspector
{
    [CustomPropertyDrawer(typeof(TowerData.TowerTiersConfig))]
    public class TowerTierConfig_Inspector : PropertyDrawer
    {
        private const string TIER_HEADER_NAME = "TowerTierHeader";

        [SerializeField] private VisualTreeAsset _treeAsset;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            TemplateContainer root = _treeAsset.CloneTree();

            Label label = root.Q<Label>(TIER_HEADER_NAME);
            int index = property.GetArrayIndex();

            if (label != null)
            {
                label.text = $"Tower Tier {index + 1} Details";
            }

            return root;
        }
    }
}