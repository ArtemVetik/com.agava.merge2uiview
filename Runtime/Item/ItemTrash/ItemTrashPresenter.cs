using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Agava.Merge2UIView
{
    internal class ItemTrashPresenter : IDisposable
    {
        private readonly ItemTrash _itemTrash;
        private readonly RemovedItemRewardTarget _rewardTarget;
        private readonly RemoveItemToggleFactory _removeItemToggleFactory;
        private readonly SelectedItem _selectedItem;
        
        private RemoveItemToggle _lastCreatedToggle;
        
        internal ItemTrashPresenter(ItemTrash itemTrash, RemovedItemRewardTarget rewardTarget, RemoveItemToggleFactory removeItemToggleFactory, SelectedItem selectedItem)
        {
            _itemTrash = itemTrash;
            _rewardTarget = rewardTarget;
            _removeItemToggleFactory = removeItemToggleFactory;
            _selectedItem = selectedItem;

            _selectedItem.Changed += OnSelectedItemChanged;
        }
        
        public void Dispose()
        {
            _selectedItem.Changed -= OnSelectedItemChanged;
            
            if (_lastCreatedToggle != null)
                _lastCreatedToggle.Switched -= OnRemoveToggleSwitched;
        }

        private void OnSelectedItemChanged()
        {
            if (_lastCreatedToggle != null && _lastCreatedToggle.Active == false)
                DestroyToggle();

            if (_selectedItem.Value == null)
                return;

            if (_lastCreatedToggle != null)
                DestroyToggle();
            
            if (_itemTrash.CanRemove(_selectedItem.Cell.Coordinate) == false)
                return;

            _lastCreatedToggle = _removeItemToggleFactory.CreateFor(_selectedItem.Value.Model);
            _lastCreatedToggle.Switched += OnRemoveToggleSwitched;
        }

        private void OnRemoveToggleSwitched(bool active)
        {
            if (active)
            {
                var selectedCell = _selectedItem.Cell;
                
                _rewardTarget.Add(selectedCell, selectedCell.Item.Model);
                _itemTrash.Remove(selectedCell);
                _selectedItem.Deselect();
            }
            else
            {
                _rewardTarget.Remove(_itemTrash.LastRemovedItemCell, _itemTrash.LastRemovedItem);
                _itemTrash.RestoreLastItem();
            }
        }

        private void DestroyToggle()
        {
            if (_lastCreatedToggle == null)
                throw new InvalidOperationException();
            
            _lastCreatedToggle.Switched -= OnRemoveToggleSwitched;
            Object.Destroy(_lastCreatedToggle.gameObject);
        }
    }
}