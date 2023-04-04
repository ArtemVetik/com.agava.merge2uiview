using Agava.Merge2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/BoardPresenter/BoardSetupFactory", fileName = "BoardSetupFactory", order = 56)]
    public class BoardSetupFactory : ScriptableObject, IBoardSetup
    {
        [SerializeField] private ScriptableShapeFactory _shapeFactory;
        [SerializeField] private ScriptableContourAlgorithmFactory _contourAlgorithmFactory;
        [SerializeField, HideInInspector] private List<ItemPosition> _items;
        [SerializeField, HideInInspector] private List<Vector2Int> _openedPositions;
        
        [SerializeField, ItemId] private string _addItemId;
        [SerializeField, Min(0)] private int _addItemLevel;

        public IShape Shape => _shapeFactory.Create();
        public IContourAlgorithm ContourAlgorithm => _contourAlgorithmFactory.Create();

        private void OnValidate()
        {
            for (int i = 0; i < _openedPositions.Count; i++)
            {
                var x = _openedPositions[i].x;
                var y = _openedPositions[i].y;

                if (x < 0 || y < 0)
                    _openedPositions[i] = new Vector2Int(Mathf.Clamp(x, 0, int.MaxValue), Mathf.Clamp(y, 0, int.MaxValue));
            }

            for (int i = 0; i < _items.Count; i++)
            {
                var x = _items[i].Position.x;
                var y = _items[i].Position.y;

                if (x < 0 || y < 0)
                    _items[i].Position = new Vector2Int(Mathf.Clamp(x, 0, int.MaxValue), Mathf.Clamp(y, 0, int.MaxValue));
            }
        }

        public IEnumerable<(MapCoordinate, (string, int))> Items() 
            => _items.Select(item => (new MapCoordinate(item.Position.x, item.Position.y), (item.Id, item.Level)));
        
        public IEnumerable<MapCoordinate> OpenedPositions() 
            => _openedPositions.Select(vector => new MapCoordinate(vector.x, vector.y));

        internal bool Opened(MapCoordinate position) 
            => _openedPositions.Contains(new Vector2Int(position.X, position.Y));

        internal void Open(MapCoordinate position)
        {
            if (Opened(position))
                throw new InvalidOperationException();
            
            _openedPositions.Add(new Vector2Int(position.X, position.Y));
        }

        internal void Close(MapCoordinate position)
        {
            if (Opened(position) == false)
                throw new InvalidOperationException();
            
            _openedPositions.Remove(new Vector2Int(position.X, position.Y));
        }

        internal void RemoveItem(MapCoordinate itemPosition)
        {
            var item = _items.FirstOrDefault(item => item.Position == new Vector2Int(itemPosition.X, itemPosition.Y));

            if (item != null)
                _items.Remove(item);
        }

        internal void AddItemFromInspectorIn(MapCoordinate position)
        {
            Vector2Int vector2IntItemPosition = new Vector2Int(position.X, position.Y);
            
            if (_items.Any(item => item.Position == vector2IntItemPosition))
                RemoveItem(position);
            
            _items.Add(new ItemPosition(vector2IntItemPosition, _addItemId, _addItemLevel));
        }

        [Serializable]
        private class ItemPosition
        {
            internal ItemPosition(Vector2Int position, string id, int level)
            {
                Position = position;
                Id = id;
                Level = level;
            }
            
            [field: SerializeField] public Vector2Int Position;
            [field: SerializeField] [field: ItemId] public string Id { get; private set; }
            [field: SerializeField] public int Level { get; private set; }
        }
    }
}
