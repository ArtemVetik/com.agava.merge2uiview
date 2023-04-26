using System.Collections.Generic;
using Agava.Merge2.Core;
using UnityEngine;
using YellowSquad.AssetPath;
using Object = UnityEngine.Object;

namespace Agava.Merge2UIView
{
    public class InventoryItemViewFactory
    {
        private readonly InventoryRepository _inventoryRepository;
        private readonly InventoryItemView _itemViewTemplate;
        private readonly Dictionary<string, ResourcesReference<Sprite>[]> _itemIcons;
        
        internal InventoryItemViewFactory(InventoryRepository inventoryRepository, InventoryItemView itemViewTemplate, 
            IEnumerable<KeyValuePair<string, ResourcesReference<Sprite>[]>> icons)
        {
            _inventoryRepository = inventoryRepository;
            _itemViewTemplate = itemViewTemplate;
            _itemIcons = new Dictionary<string, ResourcesReference<Sprite>[]>(icons);
        }

        public InventoryItemView Create(Item item)
        {
            var itemViewInstance = Object.Instantiate(_itemViewTemplate);
            itemViewInstance.Render(_itemIcons[item.Id][item.Level].Load());
            itemViewInstance.gameObject.AddComponent<InventoryItemInput>()
                .Initialize(_inventoryRepository, itemViewInstance, item);

            return itemViewInstance;
        }
    }
}