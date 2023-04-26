using System.Linq;
using Agava.Merge2.Core;
using Agava.Merge2.Tasks;
using UnityEngine;

namespace Agava.Merge2UIView
{
    internal class TaskListRoot : CompositeRoot, ITaskListRoot
    {
        [SerializeField] private string _saveKey;
        [SerializeField] private TaskView _viewTemplate;
        [SerializeField] private Transform _viewContainer;
        [SerializeField] private RewardValueFactory _rewardValue;
        [SerializeField] private MonoBehaviour _rewardCurrencyObject;

        private IBoard _board;
        private BoardView _boardView;

        public bool Initialized { get; private set; } = false;
        public TaskListPresenter TaskList { get; private set; }

        private void OnValidate()
        {
            if (_rewardCurrencyObject is not IRewardCurrency)
                _rewardCurrencyObject = null;
        }

        private void OnDestroy()
        {
            if (Initialized == false)
                return;

            TaskList?.Dispose();
        }

        public override void Compose(IMergeRoot mergeRoot)
        {
            _board = mergeRoot.Board;
            _boardView = mergeRoot.BoardView;
            
            var taskProgress = new TaskProgress(mergeRoot.Board);
            var reward = new TaskReward(_rewardCurrencyObject as IRewardCurrency, _rewardValue.Create());
            var viewFactory = new TaskViewFactory(_viewTemplate, _viewContainer, reward, taskProgress);

            TaskList = new TaskListPresenter(mergeRoot.Board, new PlayerPrefsRepository(_saveKey), reward, viewFactory, mergeRoot.BoardView);
            Initialized = true;
        }

        private void Update()
        {
            if (Initialized == false)
                return;
            
            foreach (var openedPosition in _board.OpenedCollection)
            {
                if (_board.HasItem(openedPosition) == false)
                    continue;
                
                var cellItem = _boardView.Cells.First(cell => cell.Coordinate == openedPosition).Item;
                
                if (cellItem == null)
                    continue;

                if (TaskList.TargetItems.Contains(_board.Item(openedPosition)))
                    cellItem.MarkCompleteTask();
                else
                    cellItem.UnmarkCompleteTask();
            }
        }
    }
}
