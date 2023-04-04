using Agava.Merge2.Core;
using System;

namespace Agava.Merge2UIView
{
    internal class MoveInput
    {
        private readonly BoardView _view;
        private readonly IMovePolicy _movePolicy;
        private readonly IMergePolicy _mergePolicy;
        private readonly IMergeAnimation _mergeAnimation;

        public MoveInput(BoardView view, IMovePolicy movePolicy, IMergePolicy mergePolicy, IMergeAnimation mergeAnimation)
        {
            _view = view;
            _movePolicy = movePolicy;
            _mergePolicy = mergePolicy;
            _mergeAnimation = mergeAnimation;
        }

        internal void Move(BoardCell from, BoardCell to, Action<bool> onComplete)
        {
            if (_movePolicy.CanMove(from.Coordinate, to.Coordinate))
            {
                _movePolicy.Move(from.Coordinate, to.Coordinate);
                _view.Render(to);
                onComplete?.Invoke(true);
                return;
            }

            if (_mergePolicy.CanMerge(from.Coordinate, to.Coordinate))
            {
                _mergePolicy.Merge(from.Coordinate, to.Coordinate);
                _mergeAnimation.Merge(from.PickUp(), to.PickUp(), () =>
                {
                    _view.Render(to);
                    onComplete?.Invoke(true);
                });
                return;
            }

            from.PullItem();
            onComplete?.Invoke(false);
            return;
        }
    }
}
