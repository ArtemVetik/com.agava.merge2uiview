using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class SelectView : MonoBehaviour
    {
        public abstract void Select(BoardCell cell);
        public abstract void Deselect();
        protected internal abstract void Init(ItemProduceInfoViewFactory itemProduceInfoViewFactory);
    }
}
