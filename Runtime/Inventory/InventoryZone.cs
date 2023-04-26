using UnityEngine;

namespace Agava.Merge2UIView
{
    internal class InventoryZone : MonoBehaviour
    {
        private InventoryRepository _inventoryRepository;
        
        internal bool HasFreePlace => _inventoryRepository.HasFreePlace;
        
        internal void Initialize(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public void Add(BoardCell cell)
        {
            _inventoryRepository.Add(cell);
        }
    }
}