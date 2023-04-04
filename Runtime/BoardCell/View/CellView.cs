using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class CellView : MonoBehaviour
    {
        protected internal abstract void Render(CellRenderType renderType);
    }
}
