using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/ClickCommands/DeleteCommand", fileName = "DeleteCommand", order = 56)]
    internal class DeleteCommandFactory : ClickCommandFactory
    {
        public override IClickCommand Create(IBoard board)
        {
            return new DeleteCommand(board);
        }
    }
}
