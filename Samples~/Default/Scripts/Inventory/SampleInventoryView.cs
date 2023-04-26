using Agava.Merge2.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView.Samples
{
    public class SampleInventoryView : InventoryView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _nullableItemTemplate;

        private InventoryItemViewFactory _itemViewFactory;
        private InventoryOpenPlaceButtonFactory _openPlaceButtonFactory;

        private void Awake() => Close();

        private void OnEnable() => _closeButton.onClick.AddListener(Close);
        private void OnDisable() => _closeButton.onClick.RemoveListener(Close);

        public override void Initialize(InventoryItemViewFactory itemViewFactory, InventoryOpenPlaceButtonFactory openPlaceButtonFactory)
        {
            _itemViewFactory = itemViewFactory;
            _openPlaceButtonFactory = openPlaceButtonFactory;
        }

        public override void Render(Item[] items, int openedPlaces)
        {
            int contentChildCount = _content.childCount;

            for (int i = 0; i < contentChildCount; i++)
                Destroy(_content.GetChild(i).gameObject);

            foreach (var item in items)
                _itemViewFactory.Create(item).transform.SetParent(_content);

            int freePlaces = openedPlaces - items.Length;

            for (int i = 0; i < freePlaces; i++)
                Instantiate(_nullableItemTemplate, _content);

            _openPlaceButtonFactory.Create(openedPlaces, _content);
        }

        public override void Open()
        {
            if (gameObject.activeSelf)
                return;
            
            gameObject.SetActive(true);
        }
        
        private void Close()
        {
            if (gameObject.activeSelf == false)
                return;
            
            gameObject.SetActive(false);
        }
    }
}
