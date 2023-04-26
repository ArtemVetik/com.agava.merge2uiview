using Agava.Merge2.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Agava.Merge2UIView
{
    internal class InventoryItemInput : MonoBehaviour, IPointerClickHandler
    {
        private InventoryRepository _inventoryRepository;
        private InventoryItemView _itemView;
        private Item _item;

        internal void Initialize(InventoryRepository inventoryRepository, InventoryItemView itemView, Item item)
        {
            _inventoryRepository = inventoryRepository;
            _itemView = itemView;
            _item = item;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_inventoryRepository.TryRemove(_item) == false)
                _itemView.RenderFailedRemove();
        }
    }
}