using System;
using UnityEngine;
#if USE_DOTWEEN
using DG.Tweening;
#endif

namespace Agava.Merge2UIView.Samples
{
    [CreateAssetMenu(menuName = "Merge2/Input/PullAnimation/DefaultPullAnimation", fileName = "SamplePullAnimation", order = 56)]
    public class SampleCellPullAnimationFactory : CellPullAnimationFactory
    {
#if USE_DOTWEEN
        [SerializeField] private float _duration;
#endif
        public override ICellPullAnimation Create()
        {
#if USE_DOTWEEN
            return new DoTweenCellPullAnimation(_duration);
#else
            return new CellPullAnimation();
#endif
        }

        private class CellPullAnimation : ICellPullAnimation
        {
            public void Pull(Transform item, Vector3 cellPosition, Action onComplete)
            {
                item.transform.position = cellPosition;
                onComplete?.Invoke();
            }
        }

#if USE_DOTWEEN
        private class DoTweenCellPullAnimation : ICellPullAnimation
        {
            private readonly float _duration;

            public DoTweenCellPullAnimation(float duration)
            {
                _duration = duration;
            }

            public void Pull(Transform item, Vector3 cellPosition, Action onComplete)
            {
                var parentCanvas = item.GetComponentInParent<Canvas>();

                if (parentCanvas)
                    item.transform.SetParent(parentCanvas.transform);

                item.transform.DOMove(cellPosition, _duration)
                    .OnComplete(() => onComplete?.Invoke());
            }
        }
#endif
    }
}
