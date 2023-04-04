using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class MergeAnimationFactory : ScriptableObject
    {
        public abstract IMergeAnimation Create();
    }
}
