using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView.Samples
{
    public class SampleOpeningDelayButtonView : OpeningDelayButtonView
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _info;

        private Action _onClicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public override void Init(int delay, Action onClicked)
        {
            _info.text = $"{delay}s";
            _onClicked = onClicked;
        }

        private void OnButtonClicked()
        {
            _onClicked.Invoke();
        }
    }
}
