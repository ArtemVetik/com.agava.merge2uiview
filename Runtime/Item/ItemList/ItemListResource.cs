using UnityEngine;

namespace Agava.Merge2UIView
{
    public class ItemListResource
    {
        private const string ResourcesDirectory = "Merge2View";
        private const string FileName = "ItemList";

        internal string FolderPath(string upFolder) => upFolder + ResourcesDirectory;
        internal string FilePath(string upFolder) => $"{FolderPath(upFolder)}/{FileName}";

        public ItemList Load()
        {
            return Resources.Load<ItemList>($"{ResourcesDirectory}/{FileName}");
        }
    }
}
