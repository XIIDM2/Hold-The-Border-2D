using Core.Utilities.Extentions;
using Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Utilities.CustomInspector
{
    [CustomPropertyDrawer(typeof(WaveData.WaveConfig))]
    public class WaveConfig_Inspector : PropertyDrawer
    {
        private const string WAVE_HEADER_NAME = "WaveHeader";

        [SerializeField] private VisualTreeAsset _treeAsset;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            TemplateContainer root = _treeAsset.CloneTree();

            Label label = root.Q<Label>(WAVE_HEADER_NAME);
            int index = property.GetArrayIndex();

            if (label != null)
            {
                label.text = $"Wave {index + 1} Details";
            }

            return root;
        }
    }
}