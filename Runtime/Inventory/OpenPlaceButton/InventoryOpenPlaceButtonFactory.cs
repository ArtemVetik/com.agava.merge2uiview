using UnityEngine;

namespace Agava.Merge2UIView
{
    public class InventoryOpenPlaceButtonFactory
    {
        private readonly Inventory _inventory;
        private readonly IPaySource _paySource;
        private readonly IInventoryPlacesCostList _placesCostList;
        private readonly InventoryOpenPlaceButton _openPlaceButtonTemplate;
        private readonly IInventoryViewRender _inventoryViewRender;
        
        internal InventoryOpenPlaceButtonFactory(Inventory inventory, IPaySource paySource, IInventoryPlacesCostList placesCostList, 
            InventoryOpenPlaceButton openPlaceButtonTemplate, IInventoryViewRender inventoryViewRender)
        {
            _inventory = inventory;
            _paySource = paySource;
            _placesCostList = placesCostList;
            _openPlaceButtonTemplate = openPlaceButtonTemplate;
            _inventoryViewRender = inventoryViewRender;
        }

        public InventoryOpenPlaceButton Create(int placeIndex, Transform parent)
        {
            var buttonInstance = Object.Instantiate(_openPlaceButtonTemplate, parent);
            buttonInstance.Initialize(_inventory, _inventoryViewRender, _paySource, _placesCostList.CostBy(placeIndex));

            return buttonInstance;
        }
    }
}