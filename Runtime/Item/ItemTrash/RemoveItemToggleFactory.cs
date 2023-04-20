using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    internal class RemoveItemToggleFactory
    {
        private readonly RemoveItemToggle _removeItemToggleTemplate;
        private readonly RemovedItemRewardList _rewardList;
        private readonly Transform _container;
        
        internal RemoveItemToggleFactory(RemoveItemToggle removeItemToggleTemplate, RemovedItemRewardList rewardList, Transform container)
        {
            _removeItemToggleTemplate = removeItemToggleTemplate;
            _rewardList = rewardList;
            _container = container;
        }

        internal RemoveItemToggle CreateFor(Item item)
        {
            var toggleInstance = Object.Instantiate(_removeItemToggleTemplate, _container);
            toggleInstance.Initialize(item, _rewardList.RewardBy(item));

            return toggleInstance;
        }
    }
}