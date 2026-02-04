using Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Utilities.CustomInspector
{
    [CustomPropertyDrawer(typeof(WaveData.WaveConfig.WaveUnitsConfig))]
    public class WaveUnits_Inspector : PropertyDrawer
    {
        [SerializeField] private VisualTreeAsset _treeAsset;
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            TemplateContainer root = _treeAsset.CloneTree();

            return root;
        }
    }
}