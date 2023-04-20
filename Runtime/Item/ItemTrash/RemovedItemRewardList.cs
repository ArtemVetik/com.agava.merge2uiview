using System;
using System.Collections.Generic;
using System.Linq;
using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/ItemTrash/Create RemovedItemRewardList", fileName = "RemovedItemRewardList", order = 56)]
    public class RemovedItemRewardList : ScriptableObject
    {
        [SerializeField] private List<ItemWithReward> _items;

        private IDictionary<string, IDictionary<int, int>> _itemsWithReward;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _items.ForEach(item => item.Validate());
        }
#endif

        internal int RewardBy(Item item)
        {
            _itemsWithReward ??= ConvertItemsToDictionary();
            
            if (_itemsWithReward.TryGetValue(item.Id, out var levelWithReward))
                if (levelWithReward.TryGetValue(item.Level, out int reward))
                    return reward;

            return 0;
        }

        private IDictionary<string, IDictionary<int, int>> ConvertItemsToDictionary() 
            => _items.ToDictionary(x => x.ItemId, y => y.ConvertItemLevelWithRewardToDictionary());

        [Serializable]
        private class ItemWithReward
        {
            [SerializeField, ItemId] private string _itemId;
            [SerializeField, Min(0)] private List<int> _rewardByLevel;
            
            public string ItemId => _itemId;

#if UNITY_EDITOR
            public void Validate()
            {
                var itemList = new ItemListResource().Load();
                int maxLevel = itemList.Levels().First(item => item.Key == ItemId).Value + 1;
                
                if (maxLevel < _rewardByLevel.Count)
                    _rewardByLevel.RemoveRange(maxLevel, _rewardByLevel.Count - maxLevel);
            }
#endif
            
            public IDictionary<int, int> ConvertItemLevelWithRewardToDictionary()
            {
                var dictionary = new Dictionary<int, int>();

                for (int i = 0; i < _rewardByLevel.Count; i++)
                {
                    int reward = _rewardByLevel[i];
                    dictionary.Add(i, reward);
                }

                return dictionary;
            }
        }
    }
}