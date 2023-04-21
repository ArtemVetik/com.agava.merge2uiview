using System;
#if USE_DOTWEEN
using DG.Tweening;
#endif
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    [CreateAssetMenu(menuName = "Merge2/ItemTrash/Create SimpleRemoveItemAnimationFactory", fileName = "SimpleRemoveItemAnimationFactory", order = 56)]
    public class SimpleRemoveItemAnimationFactory : RemoveItemAnimationFactory
    {
#if USE_DOTWEEN
        [SerializeField] private float _duration;
#endif
        
        public override IRemoveItemAnimation Create()
        {
#if USE_DOTWEEN
            return new DoTweenSimpleRemoveItemAnimation(_duration);
#else
            return new SimpleRemoveItemAnimation();
#endif
        }
        
        private class SimpleRemoveItemAnimation : IRemoveItemAnimation
        {
            public void Play(ItemPresenter itemPresenter, Action onComplete)
            {
                onComplete?.Invoke();
            }
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
#endif
    }
}