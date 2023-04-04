using System;
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    public class LogClickException : ClickExceptionView
    {
        public override void RenderException(Exception exception, BoardCell cell)
        {
            Debug.Log($"Click error: {exception.Message}; Cell: ({cell.Coordinate})");
        }
    }
}
