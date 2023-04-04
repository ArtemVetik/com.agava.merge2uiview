using System;

namespace Agava.Merge2UIView
{
    public class SelectedItem
    {
        private readonly SelectView[] _selectViews;

        private BoardCell _cell;

        internal SelectedItem(SelectView[] selectViews)
        {
            _selectViews = selectViews;
        }

        public ItemPresenter Value => _cell == null ? null : _cell.Item;

        internal void Select(BoardCell cell)
        {
            _cell = cell;
            Array.ForEach(_selectViews, view => view.Select(cell));
        }

        internal void Deselect()
        {
            _cell = null;
            Array.ForEach(_selectViews, view => view.Deselect());
        }
    }
}
