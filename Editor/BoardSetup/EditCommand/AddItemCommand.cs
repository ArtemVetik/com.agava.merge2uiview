using Agava.Merge2.Core;
using UnityEditor;

namespace Agava.Merge2UIView.Editor
{
    internal class AddItemCommand : IBoardPreviewEditCommand
    {
        private readonly BoardSetupFactory _boardSetupFactory;
        
        internal AddItemCommand(BoardSetupFactory boardSetupFactory)
        {
            _boardSetupFactory = boardSetupFactory;
        }
        
        public void Execute(MapCoordinate position)
        {
            _boardSetupFactory.AddItemFromInspectorIn(position);
            EditorUtility.SetDirty(_boardSetupFactory);
        }
    }
}