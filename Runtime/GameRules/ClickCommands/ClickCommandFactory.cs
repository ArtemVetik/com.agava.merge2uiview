using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class ClickCommandFactory : ScriptableObject
    {
        public abstract IClickCommand Create(IBoard board);
    }
}
