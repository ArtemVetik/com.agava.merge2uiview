using Agava.Merge2.Tasks;
using System;
using UnityEngine;

namespace Agava.Merge2UIView
{
    internal class TaskViewFactory
    {
        private readonly TaskView _template;
        private readonly Transform _container;
        private readonly TaskProgress _progress;

        internal TaskViewFactory(TaskView template, Transform container, TaskProgress progress)
        {
            _template = template;
            _container = container;
            _progress = progress;
        }

        internal TaskView Create(Task task, Action<TaskView> onCompleteClicked)
        {
            var inst = UnityEngine.Object.Instantiate(_template, _container);
            inst.Init(task, _progress, onCompleteClicked);
            inst.Render();

            return inst;
        }
    }
}
