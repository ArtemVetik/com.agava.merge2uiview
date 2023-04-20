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
            var taskProgress = new TaskProgress(mergeRoot.Board);
            var reward = new TaskReward(_rewardCurrencyObject as IRewardCurrency, _rewardValue.Create());
            var viewFactory = new TaskViewFactory(_viewTemplate, _viewContainer, reward, taskProgress);

            TaskList = new TaskListPresenter(mergeRoot.Board, new PlayerPrefsRepository(_saveKey), reward, viewFactory, mergeRoot.BoardView);
            Initialized = true;
        }
    }
}
