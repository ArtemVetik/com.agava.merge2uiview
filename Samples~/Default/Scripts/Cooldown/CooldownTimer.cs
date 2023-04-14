using Agava.Merge2.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView.Samples
{
    public class CooldownTimer : MonoBehaviour
    {
        [SerializeField] private Image _timer;

        private ItemPresenter _itemPresenter;
        private CooldownRepository _cooldownRepository;

        private void Update()
        {
            if (_itemPresenter == null)
            {
                Destroy(gameObject);
                return;
            }

            if (_cooldownRepository.Completed(_itemPresenter.Model))
            {
                _timer.enabled = false;
                return;
            }

            if (_cooldownRepository.Cooldown(_itemPresenter.Model, out Cooldown cooldown) == false)
                return;

            transform.position = _itemPresenter.transform.position;

            var progress = (float)_cooldownRepository.Remains(_itemPresenter.Model).TotalMilliseconds / (cooldown.ColldownSeconds * 1000);
            _timer.fillAmount = progress;

            _timer.enabled = true;
        }

        internal void Init(ItemPresenter itemPresenter, CooldownRepository cooldownRepository)
        {
            _itemPresenter = itemPresenter;
            _cooldownRepository = cooldownRepository;
        }
    }
}
