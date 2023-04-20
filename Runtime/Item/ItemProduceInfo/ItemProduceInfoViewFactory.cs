using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public class ItemProduceInfoViewFactory
    {
        private readonly ItemProduceInfoView _viewTemplate;
        private readonly ItemProduceInfo _produceInfo;
        private readonly OpenedItemList _openedItemList;

        internal ItemProduceInfoViewFactory(ItemProduceInfoView viewTemplate, ItemProduceInfo produceInfo, OpenedItemList openedItemList)
        {
            _viewTemplate = viewTemplate;
            _produceInfo = produceInfo;
            _openedItemList = openedItemList;
        }

        public ItemProduceInfoView CreateView(Item item)
        {
            var view = Object.Instantiate(_viewTemplate);
            view.Render(item, _produceInfo.Compute(item), _openedItemList);

            return view;
        }
    }
}
