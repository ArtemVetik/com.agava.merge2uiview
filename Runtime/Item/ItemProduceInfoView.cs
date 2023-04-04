using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class ItemProduceInfoView : MonoBehaviour
    {
        public abstract void Render(Item item, ItemProduceInfo.IResult produceInfo, OpenedItemList openedItemList);
    }
}
