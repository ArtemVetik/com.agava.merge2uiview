using UnityEngine;
using UnityEngine.EventSystems;

namespace Agava.Merge2UIView
{
    internal class CellInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerClickHandler
    {
        private BoardCell _cell;
        private BoardInput _input;
        private IDragAnimation _dragAnimation;

        public void Init(BoardCell cell, BoardInput input, IDragAnimation dragAnimation)
        {
            _cell = cell;
            _input = input;
            _dragAnimation = dragAnimation;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_input.CanDrag(_cell.Coordinate) == false)
                return;

            _input.BeginDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_input.CanDrag(_cell.Coordinate) == false)
                return;

            _dragAnimation.Drag(_cell.Item, eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_input.CanDrag(_cell.Coordinate) == false)
                return;

            var target = eventData.pointerCurrentRaycast.gameObject;
            
            if (target != null && target.TryGetComponent(out BoardCell to))
                _input.EndDrag(_cell, to);
            else if (target != null && target.TryGetComponent(out InventoryZone inventory) && inventory.HasFreePlace)
                inventory.Add(_cell);
            else
                _input.EndDrag(_cell);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.delta != Vector2.zero)
                return;

            _input.Click(_cell);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _input.PointerDown(_cell);
        }
    }
}
