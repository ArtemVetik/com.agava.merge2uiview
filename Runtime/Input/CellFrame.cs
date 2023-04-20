using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class CellFrame : MonoBehaviour, ISelectedView
    {
        protected internal abstract void Select(BoardCell cell);
        protected internal abstract void Deselect();

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
