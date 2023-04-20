using System;

namespace Agava.Merge2UIView
{
    public class SelectedItem
    {
        private readonly ISelectedView[] _selectedViews;
        private BoardCell _cell;

        public event Action Changed;

        internal SelectedItem(params ISelectedView[] selectedViews)
        {
            _selectedViews = selectedViews;
        }

        public ItemPresenter Value => _cell == null ? null : _cell.Item;
        public BoardCell Cell => _cell;

        internal void Select(BoardCell cell)
        {
            _cell = cell;
            Array.ForEach(_selectedViews, view => view.Select(cell));
            Changed?.Invoke();
        }

        internal void Deselect()
        {
            _cell = null;
            Array.ForEach(_selectedViews, view => view.Deselect());
            Changed?.Invoke();
        }
    }
}
