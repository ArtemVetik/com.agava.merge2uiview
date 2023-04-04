using Agava.Merge2.Core;
using UnityEditor;

namespace Agava.Merge2UIView.Editor
{
    internal class RemoveItemCommand : IBoardPreviewEditCommand
    {
        private readonly BoardSetupFactory _boardSetupFactory;
        
        internal RemoveItemCommand(BoardSetupFactory boardSetupFactory)
        {
            _boardSetupFactory = boardSetupFactory;
        }
        
        public void Execute(MapCoordinate position)
        {
            _boardSetupFactory.RemoveItem(position);
            EditorUtility.SetDirty(_boardSetupFactory);
        }
    }
}