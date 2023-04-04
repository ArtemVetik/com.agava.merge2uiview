using UnityEngine;
#if USE_DOTWEEN
using DG.Tweening;
#endif

namespace Agava.Merge2UIView.Samples
{
    [CreateAssetMenu(menuName = "Merge2/Input/DragAnimation/DefaultDragAnimation", fileName = "SampleDragAnimation", order = 56)]
    public class SampleDragAnimationFactory : DragAnimationFactory
    {
#if USE_DOTWEEN
        [SerializeField] private float _duration;
#endif
        public override IDragAnimation Create()
        {
#if USE_DOTWEEN
            return new DoTweenDragAnimation(_duration);
#else
            return new DragAnimation();
#endif
        }

        private class DragAnimation : IDragAnimation
        {
            private Canvas _parentCanvas;

            public void Drag(ItemPresenter item, Vector2 pointerPosition)
            {
                if (_parentCanvas == null)
                    _parentCanvas = item.GetComponentInParent<Canvas>();

                item.transform.SetParent(_parentCanvas.transform);
                item.transform.position = pointerPosition;
            }
        }

#if USE_DOTWEEN
        private class DoTweenDragAnimation : IDragAnimation
        {
            private readonly float _duration;

            private Canvas _parentCanvas;

            public DoTweenDragAnimation(float duration)
            {
                _duration = duration;
            }

            public void Drag(ItemPresenter item, Vector2 pointerPosition)
            {
                if (_parentCanvas == null)
                    _parentCanvas = item.GetComponentInParent<Canvas>();

                item.transform.SetParent(_parentCanvas.transform);
                item.transform.SetAsLastSibling();
                item.transform.DOMove(pointerPosition, _duration);
            }
        }
#endif
    }
}
