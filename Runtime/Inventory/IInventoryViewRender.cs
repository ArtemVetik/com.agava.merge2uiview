using Agava.Merge2.Core;

namespace Agava.Merge2UIView
{
    internal interface IInventoryViewRender
    {
        void Render(Item[] items, int openedPlaces);
    }
}