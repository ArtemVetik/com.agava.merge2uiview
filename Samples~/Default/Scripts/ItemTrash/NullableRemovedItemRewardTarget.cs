using Agava.Merge2.Core;

namespace Agava.Merge2UIView.Samples
{
    public class NullableRemovedItemRewardTarget : RemovedItemRewardTarget
    {
        public override void Add(BoardCell cell, Item item) { }
        public override void Remove(BoardCell cell, Item item) { }
        protected override void Initialize(RemovedItemRewardList rewardList) { }
    }
}