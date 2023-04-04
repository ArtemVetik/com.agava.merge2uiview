using Agava.Merge2.Core;
using UnityEditor;

namespace Agava.Merge2UIView.Editor
{
    internal class SwitchCellStateCommand : IBoardPreviewEditCommand
    {
        private readonly BoardSetupFactory _boardSetupFactory;
        
        internal SwitchCellStateCommand(BoardSetupFactory boardSetupFactory)
        {
            _boardSetupFactory = boardSetupFactory;
        }
        
        public void Execute(MapCoordinate position)
        {
            if (_boardSetupFactory.Opened(position))
                _boardSetupFactory.Close(position);
            else
                _boardSetupFactory.Open(position);
            
            EditorUtility.SetDirty(_boardSetupFactory);
        }
    }
}