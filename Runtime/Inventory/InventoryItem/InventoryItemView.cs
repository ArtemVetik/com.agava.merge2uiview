using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class InventoryItemView : MonoBehaviour
    {
        public abstract void Render(Sprite icon);
        public abstract void RenderFailedRemove();
    }
}