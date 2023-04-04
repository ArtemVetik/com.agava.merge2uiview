using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Agava.Merge2UIView.Editor
{
    [CustomPropertyDrawer(typeof(ItemIdAttribute))]
    internal class ItemIdAttributeDrawer : PropertyDrawer
    {
        private const string InvalidTypeLabel = "Attribute invalid for type ";
        private const string ItemListAssetNotExistLabel = "ItemIdList not created";
        private ItemListResource _itemListResource;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _itemListResource = new ItemListResource();
            
            if (property.propertyType != SerializedPropertyType.String)
            {
                DrawErrorProperty(position, label, InvalidTypeLabel + fieldInfo.FieldType.Name);
                return;
            }

            if (_itemListResource.Load() == false)
            {
                DrawErrorProperty(position, label, ItemListAssetNotExistLabel);
                return;
            }

            List<string> options = Options();
            int selected = options.IndexOf(property.stringValue);
            
            EditorGUI.BeginChangeCheck();
            selected = EditorGUI.Popup(position, label.text, selected, options.ToArray());

            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = options[selected];
                EditorGUI.PropertyField(position, property, label);
            }
        }

        private List<string> Options()
        {
            var options = new List<string>();
            var itemList = _itemListResource.Load();

            for (int i = 0; i < itemList.Count; i++)
                options.Add(itemList.ItemIdBy(i).ID);

            return options;
        }

        private void DrawErrorProperty(Rect position, GUIContent label, string message)
        {
            Rect labelPosition = position;
            labelPosition.width = EditorGUIUtility.labelWidth;

            GUI.Label(labelPosition, label);

            Rect contentPosition = position;
            contentPosition.x += labelPosition.width;
            contentPosition.width -= labelPosition.width;

            EditorGUI.HelpBox(contentPosition, message, MessageType.Error);
        }
    }
}