using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class TimerView : MonoBehaviour
    {
        public abstract void Init(ItemPresenter itemPresenter, ITimeRepository timeRepository);
    }
}
