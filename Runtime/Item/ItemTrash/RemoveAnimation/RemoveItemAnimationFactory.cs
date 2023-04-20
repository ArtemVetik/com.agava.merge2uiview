using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class RemoveItemAnimationFactory : ScriptableObject
    {
        public abstract IRemoveItemAnimation Create();
    }
}