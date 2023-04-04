using System;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class ClickExceptionView : MonoBehaviour
    {
        public abstract void RenderException(Exception exception, BoardCell cell);
    }
}
