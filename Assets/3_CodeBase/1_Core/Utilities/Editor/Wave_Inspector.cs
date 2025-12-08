using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(WaveData.Wave))]
public class Wave_Inspector : PropertyDrawer
{
    [SerializeField] private VisualTreeAsset _treeAsset;
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        TemplateContainer root = _treeAsset.CloneTree();
        return root;
    }

}
