using System;
using System.Linq;
using Agava.Merge2.Core;

namespace Agava.Merge2UIView
{
    internal class ItemTrash
    {
        private readonly Item _nullableItem = new Item("");
        
        private readonly IBoard _board;
        private readonly BoardView _boardView;
        private readonly RemoveItemRule _removeItemRule;
        private readonly IRemoveItemAnimation _removeItemAnimation;
        private readonly IRestoreItemAnimation _restoreItemAnimation;
        
        private BoardCell _lastRemovedItemCell;
        private Item _lastRemovedItem;

        internal ItemTrash(RemoveItemRule removeItemRule, ItemAnimationFactory itemAnimationFactory, IBoard board, BoardView boardView)
        {
            _board = board;
            _boardView = boardView;
            _removeItemRule = removeItemRule;
            _removeItemAnimation = itemAnimationFactory.CreateRemoveAnimation();
            _restoreItemAnimation = itemAnimationFactory.CreateRestoreAnimation();
        }

        internal BoardCell LastRemovedItemCell => _lastRemovedItemCell;
        internal Item LastRemovedItem => _lastRemovedItem;

        internal bool CanRemove(MapCoordinate itemPosition)
        {
            return _board.HasItem(itemPosition) && 
                   _board.OpenedCollection.Contains(itemPosition) &&
                   _removeItemRule.CanRemove(_board, _board.Item(itemPosition));
        }
        
        internal void Remove(BoardCell cell)
        {
            if (CanRemove(cell.Coordinate) == false)
                throw new InvalidOperationException();

            _lastRemovedItemCell = cell;
            _lastRemovedItem = _board.Item(cell.Coordinate);
            
            _board.Remove(cell.Coordinate);
            _removeItemAnimation.Play(cell.Item, onComplete: () => _boardView.Render(cell));
        }

        internal void RestoreLastItem()
        {
            if (_lastRemovedItem == _nullableItem)
                throw new InvalidOperationException();
            
            _board.Add(_lastRemovedItem, _lastRemovedItemCell.Coordinate);
            _boardView.Render(_lastRemovedItemCell);
            
            _restoreItemAnimation.Play(_lastRemovedItemCell.Item);
            
            _lastRemovedItem = _nullableItem;
            _lastRemovedItemCell = null;
        }
    }
}
