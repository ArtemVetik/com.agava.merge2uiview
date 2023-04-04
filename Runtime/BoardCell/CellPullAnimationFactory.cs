using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class CellPullAnimationFactory : ScriptableObject
    {
        public abstract ICellPullAnimation Create();
    }
}
