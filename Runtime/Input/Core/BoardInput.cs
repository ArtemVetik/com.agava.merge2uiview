using Agava.Merge2.Core;
using System;

namespace Agava.Merge2UIView
{
    internal class BoardInput
    {
        private readonly IReadOnlyBoard _board;
        private readonly MoveInput _moveInput;
        private readonly ClickInput _clickInput;
        private readonly SelectedItem _selectedItem;

        public BoardInput(IReadOnlyBoard board, MoveInput moveInput, ClickInput clickInput, SelectedItem selectedItem)
        {
            _board = board;
            _moveInput = moveInput;
            _clickInput = clickInput;
            _selectedItem = selectedItem;
        }

        internal bool CanDrag(MapCoordinate coordinate)
        {
            return _board.Opened(coordinate) && _board.HasItem(coordinate);
        }

        internal void BeginDrag()
        {
            _selectedItem.Deselect();
        }

        internal void EndDrag(BoardCell from, BoardCell to)
        {
            _moveInput.Move(from, to, (moved) =>
            {
                if (moved)
                    _selectedItem.Select(to);
                else
                    _selectedItem.Select(from);
            });
        }

        internal void EndDrag(BoardCell cell)
        {
            cell.PullItem();
            _selectedItem.Select(cell);
        }

        internal void PointerDown(BoardCell cell)
        {
            if (_board.HasItem(cell.Coordinate) && _board.Contour(cell.Coordinate))
                _selectedItem.Select(cell);
        }

        internal void Click(BoardCell cell)
        {
            if (_board.HasItem(cell.Coordinate) == false)
                return;

            if (_board.Opened(cell.Coordinate) && _selectedItem.Value == cell.Item)
            {
                _clickInput.Click(cell);

                if (_board.HasItem(cell.Coordinate) == false)
                    _selectedItem.Deselect();
            }

            if (_board.HasItem(cell.Coordinate) && _board.Opened(cell.Coordinate))
                _selectedItem.Select(cell);
        }
    }
}
