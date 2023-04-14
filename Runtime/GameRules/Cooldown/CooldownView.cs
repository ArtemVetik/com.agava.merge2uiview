using Agava.Merge2.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class CooldownView : MonoBehaviour
    {
        public abstract void Init(IReadOnlyList<BoardCell> cells, CooldownRepository cooldownRepository);
    }
}
