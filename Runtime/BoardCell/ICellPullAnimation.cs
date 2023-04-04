using System;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public interface ICellPullAnimation
    {
        void Pull(Transform item, Vector3 cellPosition, Action onComplete);
    }
}
