using System;

namespace Agava.Merge2UIView
{
    public interface IRemoveItemAnimation
    {
        void Play(ItemPresenter itemPresenter, Action onComplete);
    }
}