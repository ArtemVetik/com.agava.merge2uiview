using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/ClickCommands/LimitedProducerCommand", fileName = "LimitedProducerCommand", order = 56)]
    internal class LimitedProducerCommandFactory : ProducerCommandFactory
    {
        private const string SaveKey = "LimitedProducerCommandSaveKey";

        [SerializeField, Min(1)] private int _limit = 1;

        public override IClickCommand Create(IBoard board)
        {
            return new CommandQueue(new[]
            {
                base.Create(board),
                new LimitCommand(_limit, board, new DeleteCommand(board), new PlayerPrefsRepository(SaveKey)),
            });
        }
    }
}
