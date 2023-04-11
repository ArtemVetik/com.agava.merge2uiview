using Agava.Merge2.Tasks;
using System.Collections;
using UnityEngine;

namespace Agava.Merge2UIView
{
    internal class TaskListRoot : MonoBehaviour, ITaskListRoot
    {
        [SerializeField] private string _saveKey;
        [SerializeField] private MonoBehaviour _mergeRootObject;
        [SerializeField] private TaskView _viewTemplate;
        [SerializeField] private Transform _viewContainer;
        [SerializeField] private RewardValueFactory _rewardValue;
        [SerializeField] private MonoBehaviour _rewardCurrencyObject;

        private IMergeRoot _mergeRoot;

        public bool Initialized { get; private set; } = false;
        public TaskListPresenter TaskList { get; private set; }

        private void OnValidate()
        {
            if (_mergeRootObject is not IMergeRoot)
                _mergeRootObject = null;

            if (_rewardCurrencyObject is not IRewardCurrency)
                _rewardCurrencyObject = null;
        }

        private IEnumerator Start()
        {
            _mergeRoot = _mergeRootObject as IMergeRoot;

            yield return new WaitUntil(() => _mergeRoot.Initialized);

            var taskProgress = new TaskProgress(_mergeRoot.Board);
            var reward = new TaskReward(_rewardCurrencyObject as IRewardCurrency, _rewardValue.Create());
            var viewFactory = new TaskViewFactory(_viewTemplate, _viewContainer, reward, taskProgress);

            TaskList = new TaskListPresenter(_mergeRoot.Board, new PlayerPrefsRepository(_saveKey), reward, viewFactory, _mergeRoot.BoardView);
            Initialized = true;
        }

        private void OnDestroy()
        {
            TaskList?.Dispose();
        }
    }
}
