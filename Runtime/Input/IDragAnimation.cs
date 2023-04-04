using UnityEngine;

namespace Agava.Merge2UIView
{
    public interface IDragAnimation
    {
        void Drag(ItemPresenter item, Vector2 pointerPosition);
    }
}
