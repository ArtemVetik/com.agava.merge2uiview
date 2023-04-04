using System;
using UnityEngine;
#if USE_DOTWEEN
using DG.Tweening;
#endif

namespace Agava.Merge2UIView.Samples
{
    [CreateAssetMenu(menuName = "Merge2/Input/MergeAnimation/DefaultMergeAnimation", fileName = "SampleMergeAnimation", order = 56)]
    public class SampleMergeAnimationFactory : MergeAnimationFactory
    {
#if USE_DOTWEEN
        [SerializeField] private float _duration;
#endif

        public override IMergeAnimation Create()
        {
#if USE_DOTWEEN
            return new DoTweenMergeAnimation(_duration);
#else
            return new MergeAnimation();
#endif
        }

        private class MergeAnimation : IMergeAnimation
        {
            public void Merge(ItemPresenter from, ItemPresenter to, Action onComplete)
            {
                Destroy(from.gameObject);
                Destroy(to.gameObject);
                onComplete?.Invoke();
            }
        }

#if USE_DOTWEEN
        private class DoTweenMergeAnimation : IMergeAnimation
        {
            private readonly float _duration;

            public DoTweenMergeAnimation(float duration)
            {
                _duration = duration;
            }

            public void Merge(ItemPresenter from, ItemPresenter to, Action onComplete)
            {
                from.transform.DOMove(to.transform.position, _duration)
                    .OnComplete(() =>
                    {
                        Destroy(from.gameObject);
                        Destroy(to.gameObject);
                        onComplete?.Invoke();
                    });
            }
        }
#endif
    }
}
