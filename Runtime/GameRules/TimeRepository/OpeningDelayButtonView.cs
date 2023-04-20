using System;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class OpeningDelayButtonView : MonoBehaviour
    {
        public abstract void Init(int delay, Action onClicked);
    }
}
