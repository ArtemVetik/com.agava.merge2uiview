using System.Collections.Generic;
using System.Linq;
using Agava.Merge2.Core;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView
{
    internal class InventoryCompositeRoot : CompositeRoot
    {
        [SerializeField] private string _saveKey;
        [SerializeField, Min(0)] private int _startOpenedPlaces;
        [SerializeField] private Button _openInventoryButton;
        [SerializeField] private InventoryZone _inventoryZone;
        [SerializeField] private Object _paySourceObject;
        [SerializeField] private Object _inventoryPlacesCostListObject;
        [Header("Prefabs")]
        [SerializeField] private InventoryView _inventoryViewTemplate;
        [SerializeField] private InventoryItemView _inventoryItemViewTemplate;
        [SerializeField] private InventoryOpenPlaceButton _inventoryOpenPlaceButtonTemplate;

        private IJsonSaveRepository _saveRepository;
        private Inventory _inventory;
        private InventoryRepository _inventoryRepository;
        private InventoryView _inventoryViewInstance;
        
        private void OnValidate()
        {
            if (_paySourceObject is not IPaySource)
                _paySourceObject = null;

            if (_inventoryPlacesCostListObject is not IInventoryPlacesCostList)
                _inventoryPlacesCostListObject = null;
        }
        
        public override void Compose(IMergeRoot mergeRoot)
        {
            var itemList = new ItemListResource().Load();
            _saveRepository = new PlayerPrefsRepository(_saveKey);
            
            _inventory = _saveRepository.HasSave 
                ? JsonConvert.DeserializeObject<Inventory>(_saveRepository.Load()) 
                : new Inventory(new Dictionary<string, Item>(), _startOpenedPlaces);
            
            _inventoryViewInstance = Instantiate(_inventoryViewTemplate);

            _inventoryRepository = new InventoryRepository(_inventory, mergeRoot.Board, mergeRoot.BoardView, _inventoryViewInstance);
            var inventoryItemViewFactory = new InventoryItemViewFactory(_inventoryRepository, _inventoryItemViewTemplate, itemList.Icons());
            var inventoryOpenPlaceButtonFactory = new InventoryOpenPlaceButtonFactory(_inventory, _paySourceObject as IPaySource, 
                _inventoryPlacesCostListObject as IInventoryPlacesCostList, _inventoryOpenPlaceButtonTemplate, _inventoryViewInstance);

            _inventoryZone.Initialize(_inventoryRepository);
            _inventoryViewInstance.Initialize(inventoryItemViewFactory, inventoryOpenPlaceButtonFactory);

            _openInventoryButton.onClick.AddListener(OnOpenInventoryButtonClick);
        }

        private void OnOpenInventoryButtonClick()
        {
            _inventoryViewInstance.Render(_inventory.Items.ToArray(), _inventory.OpenedPlaces);
            _inventoryViewInstance.Open();
        }

        private void OnDestroy()
        {
            _openInventoryButton.onClick.RemoveListener(OnOpenInventoryButtonClick);
            _saveRepository.Save(JsonConvert.SerializeObject(_inventory));
        }
    }
}