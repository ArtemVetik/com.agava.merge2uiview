using UnityEngine;
#if USE_DOTWEEN
using DG.Tweening;
#endif

namespace Agava.Merge2UIView.Samples
{
    [CreateAssetMenu(menuName = "Merge2/ClickAnimations/PunchAnimation", fileName = "PunchAnimation", order = 56)]
    public class PunchClickAnimationFactory : ClickAnimationFactory
    {
        [SerializeField] private float _punchScale;
        [SerializeField] private float _duration;

        public override IClickAnimation Create()
        {
            return new PunchClickAnimation(_punchScale, _duration);
        }

        public class PunchClickAnimation : IClickAnimation
        {
            private readonly float _punchScale;
            private readonly float _duration;

            public PunchClickAnimation(float punchScale, float duration)
            {
                _punchScale = punchScale;
                _duration = duration;
            }

            public void Play(ItemPresenter item)
            {
#if USE_DOTWEEN
                item.transform.DOComplete();
                item.transform.DOPunchScale(Vector3.one * _punchScale, _duration);
#else
            Debug.Log($"There was a call to the animation method with parameters: " +
                $"{nameof(_punchScale)}: {_punchScale}, {nameof(_duration)}: {_duration}");
#endif
            }
        }
    }
}