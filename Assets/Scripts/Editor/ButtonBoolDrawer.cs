using UnityAircraft.Game.Attribute;
using UnityEditor;
using UnityEngine;

namespace UnityAircraft.Editor
{
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonBoolDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Boolean)
            {
                EditorGUI.HelpBox(position, "ButtonAttributeはbool型でのみ利用可能です", MessageType.Error);
                return;
            }

            var buttonAttribute = (ButtonAttribute)attribute;
            var title = buttonAttribute.Title ?? label.text;

            using (new GUIDisposable())
            {
                GUI.backgroundColor = property.boolValue ? Color.green : Color.red;
                if (GUI.Button(position, title))
                {
                    property.boolValue = !property.boolValue;
                }
            }
        }
    }
}
