using UnityEngine;

namespace Agava.Merge2UIView
{
    internal class ItemTrashCompositeRoot : CompositeRoot
    {
        [SerializeField] private RemoveItemToggle _removeItemToggleTemplate;
        [SerializeField] private RemovedItemRewardTarget _rewardTarget;
        [SerializeField] private RemovedItemRewardList _rewardList;
        [SerializeField] private RemoveItemRule _removeItemRule;
        [SerializeField] private RemoveItemAnimationFactory _removeItemAnimationFactory;

        private ItemTrashPresenter _itemTrashPresenter;

        public override void Compose(IMergeRoot mergeRoot)
        {
            _rewardTarget.Initialize(_rewardList);
            
            var itemTrash = new ItemTrash(_removeItemRule, _removeItemAnimationFactory.Create(), mergeRoot.Board, mergeRoot.BoardView);
            var removeItemToggleFactory = new RemoveItemToggleFactory(_removeItemToggleTemplate, _rewardList,
                mergeRoot.SelectedItemPanel.ContentContainer);
            
            _itemTrashPresenter = new ItemTrashPresenter(itemTrash, _rewardTarget, removeItemToggleFactory, mergeRoot.SelectedItem);
        }

        private void OnDestroy()
        {
            _itemTrashPresenter.Dispose();
        }
    }
}