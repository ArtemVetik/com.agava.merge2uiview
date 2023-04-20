using Agava.Merge2.Core;
using System;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/ClickCommands/OpeningDelayCommand", fileName = "OpeningDelayCommand", order = 56)]
    public class OpeningDelayCommandFactory : ClickCommandFactory
    {
        [SerializeField] private OpeningDelayRepositoryFactory _openingDelayRepository;

        public override IClickCommand Create(IBoard board)
        {
            if (_openingDelayRepository.Initialized == false)
                _openingDelayRepository.Initialize();

            return new OpeningDelayCommand(board, _openingDelayRepository.Repository);
        }
    }
}
