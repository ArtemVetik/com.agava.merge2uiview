using Agava.Merge2.Core;
using Agava.Merge2.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    public class ExampleTaskFactory : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _mergeRootObject;
        [SerializeField] private MonoBehaviour _taskRootObject;

        private IMergeRoot _mergeRoot;
        private ITaskListRoot _taskRoot;

        private void OnValidate()
        {
            if (_mergeRootObject is not IMergeRoot)
                _mergeRootObject = null;

            if (_taskRootObject is not ITaskListRoot)
                _taskRootObject = null;
        }

        private void Start()
        {
            _mergeRoot = _mergeRootObject as IMergeRoot;
            _taskRoot = _taskRootObject as ITaskListRoot;
        }

        private void Update()
        {
            if (_taskRoot.Initialized == false)
                return;

            if (_taskRoot.TaskList.Count == 0)
            {
                int taskCount = Random.Range(1, 3);

                for (int i = 0; i < taskCount; i++)
                    AddRandomTask();
            }
        }

        private void AddRandomTask()
        {
            var aviableItems = _mergeRoot.OpenedItemList.OpenedItems
                                                .Where(item => _mergeRoot.CommandFilter.FilteredId.Contains(item.Id) == false)
                                                .ToArray();

            var items = new HashSet<Item>();
            int itemCount = Random.Range(1, 3);

            for (int i = 0; i < itemCount; i++)
                items.Add(aviableItems[Random.Range(0, aviableItems.Length)]);

            _taskRoot.TaskList.Add(new Task(items.ToArray()));
        }
    }
}
