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

        private IMergeRoot _mergeRoot;

        public bool Initialized { get; private set; } = false;
        public TaskListPresenter TaskList { get; private set; }

        private void OnValidate()
        {
            if (_mergeRootObject is not IMergeRoot)
                _mergeRootObject = null;
        }

        private IEnumerator Start()
        {
            _mergeRoot = _mergeRootObject as IMergeRoot;

            yield return new WaitUntil(() => _mergeRoot.Initialized);

            var taskProgress = new TaskProgress(_mergeRoot.Board);
            var viewFactory = new TaskViewFactory(_viewTemplate, _viewContainer, taskProgress);

            TaskList = new TaskListPresenter(_mergeRoot.Board, new PlayerPrefsRepository(_saveKey), viewFactory, _mergeRoot.BoardView);
            Initialized = true;
        }

        private void OnDestroy()
        {
            TaskList?.Dispose();
        }
    }
}
