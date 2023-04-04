using Agava.Merge2.Core;
using Agava.Merge2.Tasks;
using System.Collections;
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    public class ExampleTaskFactory : MonoBehaviour
    {
        [SerializeField] private Object _taskRootObject;

        private void OnValidate()
        {
            if (_taskRootObject is not ITaskListRoot)
                _taskRootObject = null;
        }

        private IEnumerator Start()
        {
            var taskRoot = _taskRootObject as ITaskListRoot;

            yield return new WaitUntil(() => taskRoot.Initialized);

            if (taskRoot.TaskList.Count == 0)
            {
                taskRoot.TaskList.Add(new Task(new[] { new Item("Item1", 1), new Item("Item1", 2) }));
                taskRoot.TaskList.Add(new Task(new[] { new Item("Item1", 2), new Item("Item2", 1), new Item("Item3", 1) }));
            }
        }
    }
}
