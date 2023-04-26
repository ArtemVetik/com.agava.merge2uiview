using Agava.Merge2.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView
{
    public class ItemPresenter : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _taskMarkImage;

        public Item Model { get; private set; }

        internal void Init(Item item, Sprite icon)
        {
            Model = item;
            _iconImage.sprite = icon;

            UnmarkCompleteTask();
        }

        internal void MarkCompleteTask() => _taskMarkImage.enabled = true;
        internal void UnmarkCompleteTask() => _taskMarkImage.enabled = false;
    }
}
