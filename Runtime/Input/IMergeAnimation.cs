using System;

namespace Agava.Merge2UIView
{
    public interface IMergeAnimation
    {
        void Merge(ItemPresenter from, ItemPresenter to, Action onComplete);
    }
}
