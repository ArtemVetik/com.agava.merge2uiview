using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class InventoryView : MonoBehaviour, IInventoryViewRender
    {
        public abstract void Initialize(InventoryItemViewFactory itemViewFactory, InventoryOpenPlaceButtonFactory openPlaceButtonFactory);
        public abstract void Render(Item[] items, int openedPlaces);
        public abstract void Open();
    }
}