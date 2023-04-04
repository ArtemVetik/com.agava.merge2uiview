using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class ClickAnimationFactory : ScriptableObject
    {
        public abstract IClickAnimation Create();
    }
}
