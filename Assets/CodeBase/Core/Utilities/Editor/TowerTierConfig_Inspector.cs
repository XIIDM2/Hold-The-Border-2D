using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(TowerData.TowerTierConfig))]
public class TowerTierConfig_Inspector : PropertyDrawer
{
    private const string LABEL_TIER_HEADER_NAME = "TowerTierHeader";

    [SerializeField] private VisualTreeAsset _treeAsset;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        TemplateContainer root = _treeAsset.CloneTree();

        var label = root.Q<Label>(LABEL_TIER_HEADER_NAME);
        int index = property.GetArrayIndex();

        if (label != null)
        {
            label.text = $"Tower Tier {index + 1} Details";
        }

        return root;
    }   
}
