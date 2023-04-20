using Agava.Merge2.Core;
using System;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/ClickCommands/CooldownCommand", fileName = "CooldownCommand", order = 56)]
    public class CooldownCommandFactory : ClickCommandFactory
    {
        [SerializeField] private CooldownRepositoryFactory _cooldownRepository;

        public override IClickCommand Create(IBoard board)
        {
            if (_cooldownRepository.Initialized == false)
                _cooldownRepository.Initialize();

            return new CooldownCommand(board, _cooldownRepository.Repository);
        }
    }
}
