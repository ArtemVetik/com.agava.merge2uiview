using System;
using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class RemoveItemToggle : MonoBehaviour
    {
        public abstract bool Active { get; }
        
        public abstract event Action<bool> Switched;
        public abstract void Initialize(Item item, int reward);
    }
}