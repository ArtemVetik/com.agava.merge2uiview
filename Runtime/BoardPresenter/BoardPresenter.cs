using System;
using Agava.Merge2.Core;

namespace Agava.Merge2UIView
{
    internal class BoardPresenter : IDisposable
    {
        private readonly IJsonSaveRepository _saveRepository;
        private readonly IBoardSetup _boardSetup;
        private readonly JsonBoardSave _boardSave;

        internal BoardPresenter(IJsonSaveRepository saveRepository, IBoardSetup boardSetup)
        {
            _saveRepository = saveRepository;
            _boardSetup = boardSetup;
            _boardSave = new JsonBoardSave(saveRepository);
        }

        internal IBoard Board { get; private set; }

        public void Dispose()
        {
            Board.Updated -= OnBoardUpdated;
        }

        internal void Create()
        {
            if (Board != null)
                throw new InvalidOperationException("Already initialized");

            if (_saveRepository.HasSave == false)
            {
                Board = new Board(_boardSetup.Shape, _boardSetup.ContourAlgorithm, _boardSetup.OpenedPositions());

                foreach (var item in _boardSetup.Items())
                {
                    var coordinate = item.Item1;
                    var id = item.Item2.Item1;
                    var level = item.Item2.Item2;

                    Board.Add(new Item(id, level), coordinate);
                }
            }
            else
            {
                Board = _boardSave.Load(_boardSetup.Shape, _boardSetup.ContourAlgorithm);
            }

            Board.Updated += OnBoardUpdated;
        }

        private void OnBoardUpdated()
        {
            _boardSave.Save(Board);
        }
    }
}