using Agava.Merge2.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView.Samples
{
    public class SampleTimerView : TimerView
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _timer;

        private ItemPresenter _itemPresenter;
        private ITimeRepository _timeRepository;

        private void Update()
        {
            if (_itemPresenter == null)
            {
                Destroy(gameObject);
                return;
            }

            if (_timeRepository.Completed(_itemPresenter.Model))
            {
                _canvasGroup.alpha = 0f;
                _timer.enabled = false;
                return;
            }

            if (_timeRepository.Setting(_itemPresenter.Model, out int seconds) == false)
                return;

            transform.position = _itemPresenter.transform.position;

            var progress = (float)_timeRepository.Remains(_itemPresenter.Model).TotalMilliseconds / (seconds * 1000);
            _timer.fillAmount = progress;

            _timer.enabled = true;
            _canvasGroup.alpha = 1f;
        }

        public override void Init(ItemPresenter itemPresenter, ITimeRepository timeRepository)
        {
            _itemPresenter = itemPresenter;
            _timeRepository = timeRepository;
        }
    }
}
