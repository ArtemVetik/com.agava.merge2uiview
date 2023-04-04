using Agava.Merge2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/ClickCommands/ProducerCommand", fileName = "ProducerCommand", order = 56)]
    internal class ProducerCommandFactory : ClickCommandFactory
    {
        [SerializeField] private List<LevelItemList> _produceItems;

        public override IClickCommand Create(IBoard board)
        {
            var levelItems = new ProduceItems[_produceItems.Count];
            for (int i = 0; i < _produceItems.Count; i++)
            {
                var items = _produceItems[i].Items.Select(item => new Item(item.ID, item.Level));
                levelItems[i] = new ProduceItems(items.ToArray());
            }

            return new ProduceCommand(board, levelItems);
        }

        [Serializable]
        private class LevelItemList
        {
            [field: SerializeField] public ProduceItem[] Items { get; private set; }
        }

        [Serializable]
        private class ProduceItem
        {
            [SerializeField, ItemId] private string _id;
            [SerializeField] private int _level;

            public string ID => _id;
            public int Level => _level;
        }
    }
}
