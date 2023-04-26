using System;
using System.Collections.Generic;
using Agava.Merge2.Core;
using Newtonsoft.Json;

namespace Agava.Merge2UIView
{
    [Serializable]
    internal class Inventory
    {
        [JsonProperty] private readonly Dictionary<string, Item> _items;
        [JsonProperty] private int _openedPlaces;
        
        [JsonConstructor]
        internal Inventory(Dictionary<string, Item> items, int openedPlaces)
        {
            _items = items;
            _openedPlaces = openedPlaces;
        }

        internal int OpenedPlaces => _openedPlaces;
        internal bool HasFreePlace => _openedPlaces > _items.Count;
        internal IEnumerable<Item> Items => _items.Values;

        internal void Add(Item item)
        {
            if (HasFreePlace == false)
                throw new InvalidOperationException("No free place");
            
            _items.Add(item.Guid, item);
        }

        internal void Remove(Item item)
        {
            if (_items.ContainsKey(item.Guid) == false)
                throw new InvalidOperationException();
            
            _items.Remove(item.Guid);
        }

        internal void OpenPlace()
        {
            _openedPlaces += 1;
        }
    }
}
