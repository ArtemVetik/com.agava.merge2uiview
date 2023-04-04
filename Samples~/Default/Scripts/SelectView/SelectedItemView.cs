using Agava.Merge2.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView.Samples
{
    public class SelectedItemView : SelectView
    {
        [SerializeField] private TMP_Text _infoText;
        [SerializeField] private Button _button;

        private ItemProduceInfoViewFactory _itemProduceInfoViewFactory;
        private Item _selectedItem;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public override void Select(BoardCell cell)
        {
            _selectedItem = cell.Item.Model;
            _infoText.text = $"(ID: {_selectedItem.Id}, level: {_selectedItem.Level})";
            _button.interactable = true;
        }

        public override void Deselect()
        {
            _selectedItem = new Item("");
            _infoText.text = "Choose an item to see the information";
            _button.interactable = false;
        }

        protected override void Init(ItemProduceInfoViewFactory itemProduceInfoViewFactory)
        {
            _itemProduceInfoViewFactory = itemProduceInfoViewFactory;
        }

        private void OnButtonClicked()
        {
            if (_selectedItem == new Item(""))
                throw new InvalidOperationException();

            _itemProduceInfoViewFactory.CreateView(_selectedItem);
        }
    }
}
