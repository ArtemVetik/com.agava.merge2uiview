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
        [SerializeField] private TMP_Text _rewardText;

        private Task _task;
        private TaskReward _reward;
        private TaskProgress _progress;
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

        public override void Init(Task task, TaskReward reward, TaskProgress progress, Action<TaskView> completeClicked)
        {
            _task = task;
            _reward = reward;
            _progress = progress;
            _completeClicked = completeClicked;
        }

        public override void Render()
        {
            _progress.Compute(_task);
            var requiredItems = _progress.RequiredItems;
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
            _rewardText.text = $"Reward: {_reward.RewardValue(_task)}";
        }

        private void OnCompleteButtonClicked()
        {
            _completeClicked?.Invoke(this);
        }
    }
}
