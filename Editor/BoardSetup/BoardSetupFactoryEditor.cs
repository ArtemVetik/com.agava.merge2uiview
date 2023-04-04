using UnityEditor;
using UnityEngine;

namespace Agava.Merge2UIView.Editor
{
    [CustomEditor(typeof(BoardSetupFactory))]
    internal class BoardSetupFactoryEditor : UnityEditor.Editor
    {
        private const string AddItemIdPropertyName = "_addItemId";
        private const string AddItemLevelPropertyName = "_addItemLevel";

        private IBoardPreviewEditCommand _currentEditCommand;
        private BoardSetupFactory _boardSetupFactory;
        private SerializedProperty[] _addItemProperties;
        private bool _previewActive;

        private void OnEnable()
        {
            _previewActive = false;
            _boardSetupFactory = target as BoardSetupFactory;
            
            _addItemProperties = new []
            {
                serializedObject.FindProperty(AddItemIdPropertyName),
                serializedObject.FindProperty(AddItemLevelPropertyName)
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            DrawPropertiesExcluding(serializedObject, "m_Script", AddItemIdPropertyName, AddItemLevelPropertyName);
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Separator();

            if (_previewActive == false && GUILayout.Button("Preview Window"))
            {
                _previewActive = true;
                InitializeWindow(new SwitchCellStateCommand(_boardSetupFactory));
            }

            if (_previewActive == false) 
                return;
            
            GUILayout.Label("Window Editor", new GUIStyle {fontStyle = FontStyle.Bold});

            if (GUILayout.Button("Switch Cell State Mode", ButtonStyle<SwitchCellStateCommand>(_currentEditCommand)))
                InitializeWindow(new SwitchCellStateCommand(_boardSetupFactory));
            
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Add Item Mode", ButtonStyle<AddItemCommand>(_currentEditCommand)))
                InitializeWindow(new AddItemCommand(_boardSetupFactory));
            
            if (GUILayout.Button("Remove Item Mode", ButtonStyle<RemoveItemCommand>(_currentEditCommand)))
                InitializeWindow(new RemoveItemCommand(_boardSetupFactory));
            
            GUILayout.EndHorizontal();
            
            if (_currentEditCommand is AddItemCommand)
            {
                EditorGUILayout.Separator();
                
                foreach (var property in _addItemProperties)
                    EditorGUILayout.PropertyField(property);
                
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void InitializeWindow(IBoardPreviewEditCommand command)
        {
            _currentEditCommand = command;
            BoardPreviewWindow.ShowWindow(_boardSetupFactory, _currentEditCommand);
        }

        private GUIStyle ButtonStyle<TCommand>(IBoardPreviewEditCommand command) where TCommand : IBoardPreviewEditCommand
        {
            GUIStyle normalButtonStyle = new GUIStyle(GUI.skin.button);

            GUIStyle selectedButtonStyle = new GUIStyle(GUI.skin.button);
            selectedButtonStyle.normal.background = Texture2D.grayTexture;
                
            return command is TCommand ? selectedButtonStyle : normalButtonStyle;
        }
    }
}
