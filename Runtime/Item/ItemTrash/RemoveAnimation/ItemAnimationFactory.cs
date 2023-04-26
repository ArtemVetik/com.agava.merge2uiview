using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class ItemAnimationFactory : ScriptableObject
    {
        public abstract IRemoveItemAnimation CreateRemoveAnimation();
        public abstract IRestoreItemAnimation CreateRestoreAnimation();
    }
}