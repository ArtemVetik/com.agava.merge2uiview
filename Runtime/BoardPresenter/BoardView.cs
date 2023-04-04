using Agava.Merge2.Core;
using System.Collections.Generic;
using System.Linq;

namespace Agava.Merge2UIView
{
    public class BoardView
    {
        private readonly List<BoardCell> _cells;
        private readonly IReadOnlyBoard _board;
        private readonly CellFactory _cellFactory;
        private readonly ItemFactory _itemFactory;

        internal BoardView(IReadOnlyBoard board, ItemFactory itemFactory, CellFactory cellFactory)
        {
            _board = board;
            _itemFactory = itemFactory;
            _cellFactory = cellFactory;

            _cells = new List<BoardCell>();

            for (int y = 0; y < board.Height; y++)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    var coordinate = new MapCoordinate(x, y);

                    if (board.Contains(coordinate))
                        _cells.Add(_cellFactory.Create(coordinate));
                    else
                        _cellFactory.CreateEmpty(coordinate);
                }
            }
        }

        public IReadOnlyList<BoardCell> Cells => _cells;

        public void Render(BoardCell clickedCell)
        {
            var freeItems = new List<ItemPresenter>();
            var notSync = new Dictionary<BoardCell, Item>();

            foreach (var cell in _cells)
            {
                var coordinate = cell.Coordinate;
                var nullableItem = new Item("");

                Item boardItem = _board.HasItem(coordinate) ? _board.Item(coordinate) : nullableItem;
                Item cellItem = cell.Item ? cell.Item.Model : nullableItem;

                if (boardItem != cellItem)
                {
                    if (cellItem != nullableItem)
                        freeItems.Add(cell.PickUp());

                    if (boardItem != nullableItem)
                        notSync.Add(cell, boardItem);
                }
                else if (cellItem != nullableItem)
                {
                    cell.PullItem();
                }

                var renderType = RenderType(cell.Coordinate);
                cell.Render(renderType);
            }

            foreach (var error in notSync)
            {
                var freeItem = freeItems.FirstOrDefault(item => item.Model.Guid == error.Value.Guid);

                if (freeItem != null)
                {
                    error.Key.Bind(freeItem);
                    freeItems.Remove(freeItem);
                }
                else
                {
                    var item = _itemFactory.Load(_board.Item(error.Key.Coordinate));
                    item.transform.position = clickedCell.transform.position;
                    error.Key.Bind(item);
                }
            }

            foreach (var item in freeItems)
                UnityEngine.Object.Destroy(item.gameObject);
        }

        private CellRenderType RenderType(MapCoordinate coordinate)
        {
            if (_board.Opened(coordinate))
                return CellRenderType.Opened;

            if (_board.Contour(coordinate))
                return CellRenderType.Contour;

            return CellRenderType.Closed;
        }
    }
}
