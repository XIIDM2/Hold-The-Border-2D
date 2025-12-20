using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(WaveData.WaveConfig))]
public class WaveConfig_Inspector : PropertyDrawer
{
    private const string LABEL_WAVE_HEADER_NAME = "WaveHeader";

    [SerializeField] private VisualTreeAsset _treeAsset;
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        TemplateContainer root = _treeAsset.CloneTree();

        var label = root.Q<Label>(LABEL_WAVE_HEADER_NAME);
        int index = property.GetArrayIndex();

        if (label != null)
        {
            label.text = $"Wave {index + 1} Details";
        }
        return root;
    }

}
