using UnityEditor;
using UnityEngine;

namespace Agava.Merge2UIView.Editor
{
    [CustomEditor(typeof(ArbitaryShapeFactory))]
    public class ArbitaryShapeFactoryEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            base.OnInspectorGUI();
            GUI.enabled = true;

            if (GUILayout.Button("Open editor"))
                ArbitaryShapeFactoryWindow.ShowWindow(target as ArbitaryShapeFactory);
        }
    }
}
