using System.Linq;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public class CooldownCompositeRoot : CompositeRoot
    {
        [SerializeField] private RepositoryTimersView _repositoryTimersView;
        [SerializeField] private OpeningDelayButtonView _openingDelayButtonViewTemplate;
        [SerializeField] private CooldownRepositoryFactory _cooldownRepository;
        [SerializeField] private OpeningDelayRepositoryFactory _openingDelayRepository;

        private IMergeRoot _mergeRoot;
        private OpeningDelayButtonView _openingDelayButtonView;

        public override void Compose(IMergeRoot mergeRoot)
        {
            _mergeRoot = mergeRoot;

            if (_cooldownRepository.Initialized == false)
                _cooldownRepository.Initialize();

            if (_openingDelayRepository.Initialized == false)
                _openingDelayRepository.Initialize();

            var cooldownTimersView = Instantiate(_repositoryTimersView);
            var openingDelayTimersView = Instantiate(_repositoryTimersView);

            cooldownTimersView.Init(_mergeRoot.BoardView.Cells, _cooldownRepository.Repository);
            openingDelayTimersView.Init(_mergeRoot.BoardView.Cells, _openingDelayRepository.Repository);

            _mergeRoot.SelectedItem.Changed += OnSelectedItemChanged;
        }

        private void OnDestroy()
        {
            _mergeRoot.SelectedItem.Changed -= OnSelectedItemChanged;
        }

        private void OnSelectedItemChanged()
        {
            if (_openingDelayButtonView)
                Destroy(_openingDelayButtonView.gameObject);

            if (_mergeRoot.SelectedItem.Value == null)
                return;

            var currentItem = _mergeRoot.SelectedItem.Value.Model;

            if (_openingDelayRepository.Repository.Setting(currentItem, out int delay) == false)
                return;

            if (_openingDelayRepository.Repository.Items.Contains(currentItem.Guid))
                return;

            var isOpened = _mergeRoot.Board.OpenedCollection.Any(coordinate =>
                _mergeRoot.Board.HasItem(coordinate) && _mergeRoot.Board.Item(coordinate).Guid == currentItem.Guid);

            if (isOpened == false)
                return;

            _openingDelayButtonView = Instantiate(_openingDelayButtonViewTemplate, _mergeRoot.SelectedItemPanel.ContentContainer);
            _openingDelayButtonView.Init(delay, () =>
            {
                _openingDelayRepository.Repository.Open(currentItem);
                Destroy(_openingDelayButtonView.gameObject);
            });
        }
    }
}