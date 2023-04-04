using Agava.Merge2.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Text;

namespace Agava.Merge2UIView.Samples
{
    public class SimpleTaskView : TaskView
    {
        [SerializeField] private Button _completeButton;
        [SerializeField] private TMP_Text _info;

        private Task _task;
        private TaskProgress _taskProgress;
        private Action<TaskView> _completeClicked;

        public override Task Model => _task;

        private void OnEnable()
        {
            _completeButton.onClick.AddListener(OnCompleteButtonClicked);
        }

        private void OnDisable()
        {
            _completeButton.onClick.RemoveListener(OnCompleteButtonClicked);
        }

        public override void Init(Task task, TaskProgress progress, Action<TaskView> completeClicked)
        {
            _task = task;
            _taskProgress = progress;
            _completeClicked = completeClicked;
        }

        public override void Render()
        {
            _taskProgress.Compute(_task);
            var requiredItems = _taskProgress.RequiredItems;
            _info.text = string.Empty;

            var text = new StringBuilder();
            foreach (var item in _task.TotalItems)
            {
                text.Append($"{item.Id}:{item.Level}");

                if (requiredItems.Contains(item) == false)
                    text.Append(" (*)");

                text.Append('\n');
            }

            _info.text = text.ToString();
        }

        private void OnCompleteButtonClicked()
        {
            _completeClicked?.Invoke(this);
        }
    }
}
