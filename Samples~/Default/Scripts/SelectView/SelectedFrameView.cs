using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView.Samples
{
    public class SelectedFrameView : SelectView
    {
        [SerializeField] private Image _cellFrame;

        public override void Select(BoardCell cell)
        {
            _cellFrame.transform.SetParent(cell.transform);
            _cellFrame.transform.localPosition = Vector3.zero;
            _cellFrame.enabled = true;
        }

        public override void Deselect()
        {
            _cellFrame.enabled = false;
        }

        protected override void Init(ItemProduceInfoViewFactory itemProduceInfoViewFactory) { }
    }
}
