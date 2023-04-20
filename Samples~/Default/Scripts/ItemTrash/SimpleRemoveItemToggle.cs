using System;
using Agava.Merge2.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView.Samples
{
    public class SimpleRemoveItemToggle : RemoveItemToggle
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private TMP_Text _rewardText;
        [SerializeField] private RemovedItemPanel _removedItemPanelTemplate;

        private bool _active;
        private int _reward;
        private RemovedItemPanel _removedItemPanelInstance;

        public override bool Active => _active;
        public override event Action<bool> Switched; 

        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        public override void Initialize(Item item,int reward)
        {
            _reward = reward;
            _button.image.color = reward > 0 ? Color.green : Color.red;
            Render();
        }

        private void OnButtonClick()
        {
            _active = _active == false;
            Render();
            
            Switched?.Invoke(_active);
        }

        private void Render()
        {
            _buttonText.text = _active ? "Restore" : "Remove";
            _rewardText.text = _reward <= 0 ? " " : _active ? $"-{_reward}" : $"+{_reward}";

            if (_active)
            {
                _removedItemPanelInstance = Instantiate(_removedItemPanelTemplate);
                _removedItemPanelInstance.Setup(this);
            }
            else if (_removedItemPanelInstance != null)
            {
                Destroy(_removedItemPanelInstance.gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_removedItemPanelInstance != null)
                Destroy(_removedItemPanelInstance.gameObject);
        }
    }
}