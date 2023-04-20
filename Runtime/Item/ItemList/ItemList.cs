using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YellowSquad.AssetPath;

namespace Agava.Merge2UIView
{
    public class ItemList : ScriptableObject
    {
        [SerializeField] private List<Item> _items;
        
        internal int Count => _items.Count;

        internal Item ItemIdBy(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            return _items[index];
        }

        public IEnumerable<KeyValuePair<string, int>> Levels()
        {
            return _items.Select(item => new KeyValuePair<string, int>(item.ID, item.Icons.Length - 1));
        }

        public IEnumerable<KeyValuePair<string, ResourcesReference<Sprite>[]>> Icons()
        {
            return _items.Select(item => new KeyValuePair<string, ResourcesReference<Sprite>[]>(item.ID, item.Icons));
        }

        [Serializable]
        internal class Item
        {
            [field: SerializeField] public string ID { get; private set; }
            [field: SerializeField] public ResourcesReference<Sprite>[] Icons { get; private set; }
        }
    }
}
