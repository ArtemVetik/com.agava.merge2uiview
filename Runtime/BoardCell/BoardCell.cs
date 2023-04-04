using Agava.Merge2.Core;
using System;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public class BoardCell : MonoBehaviour
    {
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private CellView _view;

        private ItemPresenter _item;
        private ICellPullAnimation _pullAnimation;

        public MapCoordinate Coordinate { get; private set; }
        public ItemPresenter Item => _item;

        public void PullItem()
        {
            if (_item == null)
                throw new InvalidOperationException("There is no item in the cell");

            if (_item.transform.parent == _itemContainer && _item.transform.localPosition == Vector3.zero)
                return;

            _pullAnimation.Pull(_item.transform, _itemContainer.transform.position, () =>
            {
                _item.transform.SetParent(_itemContainer);
                _item.transform.localPosition = Vector3.zero;
            });
        }

        internal void Init(MapCoordinate coordinate, ICellPullAnimation pullAnimation)
        {
            Coordinate = coordinate;
            _pullAnimation = pullAnimation;
        }

        internal void Bind(ItemPresenter item)
        {
            if (_item != null)
                throw new InvalidOperationException();

            _item = item;
            PullItem();
        }

        internal ItemPresenter PickUp()
        {
            if (_item == null)
                throw new InvalidOperationException();

            var copyLink = _item;
            _item = null;

            return copyLink;
        }

        internal void Render(CellRenderType renderType)
        {
            _view.Render(renderType);
        }
    }
}
