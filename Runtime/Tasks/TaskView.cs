using Agava.Merge2.Tasks;
using System;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class TaskView : MonoBehaviour
    {
        public abstract Task Model { get; }
        public abstract void Init(Task task, TaskReward reward, TaskProgress progress, Action<TaskView> completeClicked);
        public abstract void Render();
    }
}
