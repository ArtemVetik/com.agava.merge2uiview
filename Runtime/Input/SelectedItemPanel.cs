using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class SelectedItemPanel : MonoBehaviour, ISelectedView
    {
        public abstract Transform ContentContainer { get; }

        protected internal abstract void Select(BoardCell cell);
        protected internal abstract void Deselect();
        protected internal abstract void Init(ItemProduceInfoViewFactory itemProduceInfoViewFactory);

        void ISelectedView.Select(BoardCell cell)
        {
            Select(cell);
        }

        void ISelectedView.Deselect()
        {
            Deselect();
        }
    }
}