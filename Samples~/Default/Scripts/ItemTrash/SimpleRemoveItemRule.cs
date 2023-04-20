using System;
using System.Collections.Generic;
using System.Linq;
using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    [CreateAssetMenu(menuName = "Merge2/ItemTrash/Create SimpleRemoveItemRule", fileName = "SimpleRemoveItemRule", order = 56)]
    public class SimpleRemoveItemRule : RemoveItemRule
    {
        [SerializeField] private List<Pair> _nonRemovableItems;

        public override bool CanRemove(IReadOnlyBoard _, Item item)
        {
            return _nonRemovableItems.Any(i => item.Equals(new Item(i.ItemId, i.ItemLevel))) == false;
        }

        [Serializable]
        private class Pair
        {
            [field: SerializeField, ItemId] public string ItemId { get; private set; }
            [field: SerializeField, Min(0)] public int ItemLevel { get; private set; }
        }
    }
}