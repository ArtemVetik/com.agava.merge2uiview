using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class RemovedItemRewardTarget : MonoBehaviour
    {
        public abstract void Add(BoardCell cell, Item item);
        public abstract void Remove(BoardCell cell, Item item);
        protected internal abstract void Initialize(RemovedItemRewardList rewardList);
    }
}