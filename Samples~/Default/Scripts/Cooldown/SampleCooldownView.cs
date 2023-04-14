using Agava.Merge2.Core;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    public class SampleCooldownView : CooldownView
    {
        [SerializeField] private CooldownTimer _timerTemplate;
        [SerializeField] private Canvas _timerCanvas;

        private IReadOnlyList<BoardCell> _cells;
        private Dictionary<string, CooldownTimer> _timers;
        private CooldownRepository _repository;

        public override void Init(IReadOnlyList<BoardCell> cells, CooldownRepository cooldownRepository)
        {
            _cells = cells;
            _repository = cooldownRepository;

            _timers = new Dictionary<string, CooldownTimer>();
            foreach (var cell in cells.Where(cell => cell.Item != null))
            {
                if (_repository.CooldownItems.Contains(cell.Item.Model.Guid) == false)
                    continue;

                var timer = Instantiate(_timerTemplate, _timerCanvas.transform);
                timer.Init(cell.Item, _repository);
                _timers.Add(cell.Item.Model.Guid, timer);
            }
        }

        private void Update()
        {
            foreach (var cell in _cells.Where(cell => cell.Item != null))
            {
                if (_repository.CooldownItems.Contains(cell.Item.Model.Guid) == false)
                    continue;

                if (_timers.ContainsKey(cell.Item.Model.Guid))
                    continue;

                var timer = Instantiate(_timerTemplate, _timerCanvas.transform);
                timer.Init(cell.Item, _repository);
                _timers.Add(cell.Item.Model.Guid, timer);
            }
        }
    }
}
