using Agava.Merge2.Core;
using System;

namespace Agava.Merge2UIView
{
    internal class OpenedItemListPresenter : IDisposable
    {
        private readonly IBoard _board;
        private readonly OpenedItemListSave _openedItemListSave;

        public OpenedItemListPresenter(IJsonSaveRepository saveRepository, IBoard board)
        {
            _board = board;
            _openedItemListSave = new OpenedItemListSave(saveRepository);

            _board.Updated += OnBoardUpdated;
        }

        public OpenedItemList OpenedItemList { get; private set; }

        public void Create()
        {
            if (OpenedItemList != null)
                throw new InvalidOperationException();

            OpenedItemList = _openedItemListSave.Load(_board);
        }

        public void Dispose()
        {
            OpenedItemList.Dispose();
            _board.Updated -= OnBoardUpdated;
        }

        private void OnBoardUpdated()
        {
            _openedItemListSave.Save(OpenedItemList);
        }
    }
}
