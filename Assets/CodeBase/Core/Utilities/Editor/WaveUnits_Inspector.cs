using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(WaveData.Wave.WaveUnits))]
public class WaveUnits_Inspector : PropertyDrawer
{
    [SerializeField] private VisualTreeAsset _treeAsset;
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        TemplateContainer root = _treeAsset.CloneTree();
        return root;
    }
}
