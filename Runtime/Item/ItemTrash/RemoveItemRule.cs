using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class RemoveItemRule : ScriptableObject
    {
        public abstract bool CanRemove(IReadOnlyBoard board, Item item);
    }
}