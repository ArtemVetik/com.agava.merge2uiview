using System;
using System.Linq;
using Agava.Merge2.Core;

namespace Agava.Merge2UIView
{
    internal class InventoryRepository
    {
        private readonly Inventory _inventory;
        private readonly IBoard _board;
        private readonly BoardView _boardView;
        private readonly IInventoryViewRender _inventoryViewRender;

        internal InventoryRepository(Inventory inventory, IBoard board, BoardView boardView, IInventoryViewRender inventoryViewRender)
        {
            _inventory = inventory;
            _board = board;
            _boardView = boardView;
            _inventoryViewRender = inventoryViewRender;
        }

        internal bool HasFreePlace => _inventory.HasFreePlace;

        internal void Add(BoardCell cell)
        {
            if (HasFreePlace == false)
                throw new InvalidOperationException();
            
            _inventory.Add(cell.Item.Model);
            _board.Remove(cell.Coordinate);
            _boardView.Render(cell);
        }

        internal bool TryRemove(Item item)
        {
            if (_board.OpenedCollection.All(position => _board.HasItem(position))) 
                return false;
            
            var cell = _boardView.Cells.First(cell => cell.Item == null);
            _board.Add(item, cell.Coordinate);
            _boardView.Render(cell);
            _inventory.Remove(item);
            
            _inventoryViewRender.Render(_inventory.Items.ToArray(), _inventory.OpenedPlaces);
            
            return true;
        }
    }
}