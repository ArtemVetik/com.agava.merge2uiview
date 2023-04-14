using Agava.Merge2.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YellowSquad.AssetPath;

namespace Agava.Merge2UIView.Samples
{
    public class SampleItemProduceInfoView : ItemProduceInfoView
    {
        [SerializeField] private Sprite _nullableItemIcon;
        [SerializeField] private ItemsIconGroup _itemGroupTemplate;
        [SerializeField] private Transform _itemGroupContainer;
        [SerializeField] private Button _closeButton;

        private Dictionary<string, ResourcesReference<Sprite>[]> _icons;
        private OpenedItemList _openedItemList;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public override void Render(Item item, ItemProduceInfo.IResult produceInfo, OpenedItemList openedItemList)
        {
            var itemList = new ItemListResource().Load();
            _icons = new Dictionary<string, ResourcesReference<Sprite>[]>(itemList.Icons());
            _openedItemList = openedItemList;

            SpawnItemsGroup($"Level {item.Level + 1}", AllItemsWith(item.Id, _icons[item.Id].Length), item.Level);
            SpawnItemsGroup("Produced items", produceInfo.ProducedItems);
            SpawnItemsGroup("Produce by", produceInfo.ProduceBy);
        }

        private void SpawnItemsGroup(string label, IReadOnlyList<Item> items, int selectedIndex = -1)
        {
            if (_openedItemList.OpenedItems.Count(items.Contains) == 0)
                return;
            
            var icons = new List<Sprite>();

            foreach (var item in items)
                icons.Add(_openedItemList.OpenedItems.Contains(item) == false ? _nullableItemIcon
                    : _icons[item.Id][item.Level].Load());

            var spawnedGroup = Instantiate(_itemGroupTemplate, _itemGroupContainer);
            spawnedGroup.Render(label, icons.ToArray(), selectedIndex);
        }

        private IReadOnlyList<Item> AllItemsWith(string id, int maxLevel)
        {
            var items = new Item[maxLevel];

            for (int i = 0; i < items.Length; i++)
                items[i] = new Item(id, i);

            return items;
        }

        private void OnCloseButtonClicked()
        {
            Destroy(gameObject);
        }
    }
}
