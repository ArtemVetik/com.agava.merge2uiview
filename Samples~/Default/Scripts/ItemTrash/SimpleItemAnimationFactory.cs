using System;
#if USE_DOTWEEN
using DG.Tweening;
#endif
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    [CreateAssetMenu(menuName = "Merge2/ItemTrash/Create SimpleItemAnimationFactory", fileName = "SimpleItemAnimationFactory", order = 56)]
    public class SimpleItemAnimationFactory : ItemAnimationFactory
    {
#if USE_DOTWEEN
        [SerializeField] private float _duration;
#endif
        
        public override IRemoveItemAnimation CreateRemoveAnimation()
        {
#if USE_DOTWEEN
            return new DoTweenSimpleRemoveItemAnimation(_duration);
#else
            return new SimpleRemoveItemAnimation();
#endif
        }
        
        public override IRestoreItemAnimation CreateRestoreAnimation()
        {
#if USE_DOTWEEN
            return new DoTweenSimpleRestoreItemAnimation(_duration);
#else
            return new SimpleRestoreItemAnimation();
#endif
        }
        
        private class SimpleRemoveItemAnimation : IRemoveItemAnimation
        {
            public void Play(ItemPresenter itemPresenter, Action onComplete)
            {
                onComplete?.Invoke();
            }
        }

        private class SimpleRestoreItemAnimation : IRestoreItemAnimation
        {
            public void Play(ItemPresenter itemPresenter) { }
        }

#if USE_DOTWEEN
        private class DoTweenSimpleRemoveItemAnimation : IRemoveItemAnimation
        {
            private readonly float _duration;
            
            public DoTweenSimpleRemoveItemAnimation(float duration)
            {
                _duration = duration;
            }
            
            public void Play(ItemPresenter itemPresenter, Action onComplete)
            {
                itemPresenter.transform
                    .DOScale(Vector3.zero, _duration)
                    .OnComplete(() => onComplete?.Invoke());
            }
        }

        private class DoTweenSimpleRestoreItemAnimation : IRestoreItemAnimation
        {
            private readonly float _duration;
            
            public DoTweenSimpleRestoreItemAnimation(float duration)
            {
                _duration = duration;
            }
            
            public void Play(ItemPresenter itemPresenter)
            {
                itemPresenter.transform.DOComplete(true);
                itemPresenter.transform.DOScale(Vector3.one, _duration);
            }
        }
#endif
    }
}