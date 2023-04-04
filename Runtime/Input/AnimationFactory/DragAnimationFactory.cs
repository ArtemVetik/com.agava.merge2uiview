using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class DragAnimationFactory : ScriptableObject
    {
        public abstract IDragAnimation Create();
    }
}
