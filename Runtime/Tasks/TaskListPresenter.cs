using UnityEngine;
using Agava.Merge2.Tasks;
using System.Collections.Generic;
using Agava.Merge2.Core;
using System;

namespace Agava.Merge2UIView
{
    public class TaskListPresenter : IDisposable
    {
        private readonly IBoard _board;
        private readonly TaskViewFactory _viewFactory;
        private readonly BoardView _boardView;
        private readonly List<TaskView> _taskView;
        private readonly TaskList _taskList;
        private readonly JsonTaskListSave _taskListSave;

        internal TaskListPresenter(IBoard board, IJsonSaveRepository saveRepository, TaskReward taskReward, TaskViewFactory viewFactory, BoardView boardView)
        {
            _board = board;
            _viewFactory = viewFactory;
            _boardView = boardView;

            _taskListSave = new JsonTaskListSave(saveRepository);
            _taskList = _taskListSave.Load(_board, taskReward);
            _taskView = new List<TaskView>();

            foreach (var task in _taskList.Tasks)
            {
                var view = _viewFactory.Create(task, Complete);
                _taskView.Add(view);
            }

            _board.Updated += OnBoardUpdated;
        }

        public int Count => _taskList.Tasks.Count;

        public void Dispose()
        {
            _board.Updated -= OnBoardUpdated;
        }

        public void Add(Task task)
        {
            _taskList.Add(task);

            var view = _viewFactory.Create(task, Complete);
            _taskView.Add(view);

            _taskListSave.Save(_taskList);
        }

        public void Remove(Task task)
        {
            _taskList.Remove(task);

            var view = _taskView.Find(task => task.Model.Equals(task));
            _taskView.Remove(view);
            UnityEngine.Object.Destroy(view.gameObject);

            _taskListSave.Save(_taskList);
        }

        internal void Complete(TaskView view)
        {
            _taskList.Complete(view.Model);
            _taskView.Remove(view);
            UnityEngine.Object.Destroy(view.gameObject);

            _boardView.Render(_boardView.Cells[0]);
            _taskListSave.Save(_taskList);
        }

        private void OnBoardUpdated()
        {
            foreach (var view in _taskView)
                view.Render();
        }
    }
}
