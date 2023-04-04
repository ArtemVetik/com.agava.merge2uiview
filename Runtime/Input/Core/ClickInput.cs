using Agava.Merge2.Core;
using System;
using System.Collections.Generic;

namespace Agava.Merge2UIView
{
    internal class ClickInput
    {
        private readonly BoardView _view;
        private readonly ClickPolicy _clickPolicy;
        private readonly ClickExceptionView _clickException;
        private readonly IDictionary<string, IClickAnimation> _clickAnimations;

        public ClickInput(BoardView view, ClickPolicy clickPolicy, ClickExceptionView clickException, IEnumerable<KeyValuePair<string, IClickAnimation>> clickAnimations)
        {
            _view = view;
            _clickPolicy = clickPolicy;
            _clickException = clickException;
            _clickAnimations = new Dictionary<string, IClickAnimation>(clickAnimations);
        }

        internal void Click(BoardCell cell)
        {
            try
            {
                _clickPolicy.Click(cell.Coordinate);
                _view.Render(cell);

                if (cell.Item && _clickAnimations.TryGetValue(cell.Item.Model.Id, out IClickAnimation animation))
                    animation.Play(cell.Item);
            }
            catch (Exception exception)
            {
                _clickException.RenderException(exception, cell);
            }
        }
    }
}
