using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class CompositeRoot : MonoBehaviour
    {
        public abstract void Compose(IMergeRoot mergeRoot);
    }
}
