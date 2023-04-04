using System.IO;
using UnityEditor;
using UnityEngine;

namespace Agava.Merge2UIView.Editor
{
    internal class ItemListAsset
    {
        private const string ResourcePath = "Assets/Resources/";
        private const string FileExtension = ".asset";

        private readonly ItemListResource _itemListResource;

        public ItemListAsset()
        {
            _itemListResource = new ItemListResource();
        }

        internal void CreateAsset()
        {
            var folder = _itemListResource.FolderPath(ResourcePath);
            
            if (Directory.Exists(folder) == false)
                Directory.CreateDirectory(folder);

            ItemList itemList = ScriptableObject.CreateInstance<ItemList>();

            Debug.Log(_itemListResource.FilePath(ResourcePath) + FileExtension);
            AssetDatabase.CreateAsset(itemList, _itemListResource.FilePath(ResourcePath) + FileExtension);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}