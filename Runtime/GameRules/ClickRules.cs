using Agava.Merge2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/ClickRules", fileName = "ClickRules", order = 56)]
    internal class ClickRules : ScriptableObject
    {
        [SerializeField] private List<ClickItem> _items;

        internal (string, IClickCommand)[] ClickCommands(IBoard board)
        {
            var result = new List<(string, IClickCommand)>();

            foreach (var item in _items)
            {
                var clickCommand = new CommandQueue(item.ClickCommandsFactory.Select(factory => factory.Create(board)).ToArray());
                result.Add((item.ID, clickCommand));
            }

            return result.ToArray();
        }

        internal IEnumerable<KeyValuePair<string, IClickAnimation>> ClickAnimations()
        {
            return _items.Where(item => item.ClickAnimationFactory != null)
                .Select(item => new KeyValuePair<string, IClickAnimation>(item.ID, item.ClickAnimationFactory.Create()));
        }

        [Serializable]
        internal class ClickItem
        {
            [field: SerializeField, ItemId] public string ID { get; private set; }
            [field: SerializeField] public ClickAnimationFactory ClickAnimationFactory { get; private set; }
            [field: SerializeField] public ClickCommandFactory[] ClickCommandsFactory { get; private set; }
        }
    }
}
