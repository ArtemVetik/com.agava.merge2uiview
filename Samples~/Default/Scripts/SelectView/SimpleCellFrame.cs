using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView.Samples
{
    public class SimpleCellFrame : CellFrame
    {
        [SerializeField] private Image _cellFrame;

        protected override void Select(BoardCell cell)
        {
            _cellFrame.transform.SetParent(cell.transform);
            _cellFrame.transform.localPosition = Vector3.zero;
            _cellFrame.enabled = true;
        }

        protected override void Deselect()
        {
            _cellFrame.enabled = false;
        }
    }
}
