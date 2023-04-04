using UnityEditor;

namespace Agava.Merge2UIView.Editor
{
    internal static class ItemListEditorButton
    {
        private const string EditorPath = "Merge2View/ItemIdList";

        [MenuItem(EditorPath)]
        private static void OnButtonClick()
        {
            var itemListAsset = new ItemListResource();
            var resource = itemListAsset.Load();

            if (resource == null)
                new ItemListAsset().CreateAsset();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = itemListAsset.Load();
        }
    }
}