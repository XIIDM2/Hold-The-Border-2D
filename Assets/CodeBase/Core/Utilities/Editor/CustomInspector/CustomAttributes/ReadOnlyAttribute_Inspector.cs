using Core.Utilities.CustomProperties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Core.Utilities.CustomInspector.CustomProperties
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttribute_Inspector : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            PropertyField field = new PropertyField(property, property.displayName);
            field.SetEnabled(false);

            root.Add(field);

            return root;
        }
    }
}