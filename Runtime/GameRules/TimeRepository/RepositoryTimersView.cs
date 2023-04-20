using Agava.Merge2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public class RepositoryTimersView : MonoBehaviour
    {
        [SerializeField] private TimerView _timerTemplate;
        [SerializeField] private Canvas _timerCanvas;

        private IReadOnlyList<BoardCell> _cells;
        private Dictionary<string, TimerView> _timers;
        private ITimeRepository _timeRepository;

        public void Init(IReadOnlyList<BoardCell> cells, ITimeRepository timeRepository)
        {
            _cells = cells;
            _timeRepository = timeRepository;

            _timers = new Dictionary<string, TimerView>();
            foreach (var cell in cells.Where(cell => cell.Item != null))
            {
                if (_timeRepository.Items.Contains(cell.Item.Model.Guid) == false)
                    continue;

                var timer = Instantiate(_timerTemplate, _timerCanvas.transform);
                timer.Init(cell.Item, _timeRepository);
                _timers.Add(cell.Item.Model.Guid, timer);
            }
        }

        private void Update()
        {
            foreach (var cell in _cells.Where(cell => cell.Item != null))
            {
                if (_timeRepository.Items.Contains(cell.Item.Model.Guid) == false)
                    continue;

                if (_timers.ContainsKey(cell.Item.Model.Guid))
                    continue;

                var timer = Instantiate(_timerTemplate, _timerCanvas.transform);
                timer.Init(cell.Item, _timeRepository);
                _timers.Add(cell.Item.Model.Guid, timer);
            }
        }
    }
}
