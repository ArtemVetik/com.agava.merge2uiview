using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    internal class CellFactory
    {
        private readonly Transform _container;
        private readonly BoardCell _template;
        private readonly ICellPullAnimation _pullAnimation;

        public CellFactory(Transform container, BoardCell template, ICellPullAnimation pullAnimation)
        {
            _container = container;
            _template = template;
            _pullAnimation = pullAnimation;
        }

        public BoardCell Create(MapCoordinate coordinate)
        {
            var cell = Object.Instantiate(_template, _container);
            cell.name = $"Cell {coordinate}";
            cell.Init(coordinate, _pullAnimation);

            return cell;
        }

        public void CreateEmpty(MapCoordinate coordinate)
        {
            var emptyCell = new GameObject($"Empty Cell {coordinate}").AddComponent<RectTransform>();
            emptyCell.transform.SetParent(_container);
        }
    }
}
