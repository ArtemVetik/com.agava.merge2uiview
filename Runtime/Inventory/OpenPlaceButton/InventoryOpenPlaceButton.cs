using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView
{
    public class InventoryOpenPlaceButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private InventoryOpenPlaceButtonView _view;

        private Inventory _inventory;
        private IInventoryViewRender _inventoryViewRender;
        private IPaySource _paySource;
        private int _placeCost;
        
        public event Action<bool> Clicked;

        internal void Initialize(Inventory inventory, IInventoryViewRender inventoryViewRender, IPaySource paySource, int placeCost)
        {
            _inventory = inventory;
            _inventoryViewRender = inventoryViewRender;
            _paySource = paySource;
            _placeCost = placeCost;
            
            _view.Render(placeCost);
        }
        
        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick()
        {
            bool result = _paySource.Has(_placeCost);

            if (result)
            {
                _paySource.Pay(_placeCost);
                _inventory.OpenPlace();
                _inventoryViewRender.Render(_inventory.Items.ToArray(), _inventory.OpenedPlaces);
            }
            
            Clicked?.Invoke(result);
        }
    }
}