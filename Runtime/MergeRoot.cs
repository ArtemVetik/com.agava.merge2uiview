using Agava.Merge2.Core;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView
{
    internal class MergeRoot : MonoBehaviour, IMergeRoot
    {
        private const string OpenedItemListSaveKey = "_OpenedItemsList";
        
        [SerializeField] private string _prefsSaveKey;
        [Header("Prefabs")]
        [SerializeField] private BoardCell _cellTemplate;
        [SerializeField] private ItemPresenter _itemTemplate;
        [SerializeField] private ItemProduceInfoView _itemProduceInfoView;
        [SerializeField] private SelectView[] _selectViews;
        [Header("Board")]
        [SerializeField] private GridLayoutGroup _cellGrid;
        [SerializeField] private BoardSetupFactory _boardSetup;
        [Header("Animations")]
        [SerializeField] private DragAnimationFactory _dragAnimationFactory;
        [SerializeField] private MergeAnimationFactory _mergeAnimationFactory;
        [SerializeField] private CellPullAnimationFactory _cellPullAnimationFactory;
        [Header("Game Rule")]
        [SerializeField] private ClickRules _gameRules;
        [Header("Click exception view")]
        [SerializeField] private ClickExceptionView _clickExceptionView;

        private BoardPresenter _boardPresenter;
        private OpenedItemListPresenter _openedItemListPresenter;

        public bool Initialized { get; private set; } = false;
        public IBoard Board { get; private set; }
        public BoardView BoardView { get; private set; }
        public SelectedItem SelectedItem { get; private set; }
        public CommandFilter CommandFilter { get; private set; }
        public OpenedItemList OpenedItemList => _openedItemListPresenter.OpenedItemList;

        private void Awake()
        {
            _boardPresenter = new BoardPresenter(new PlayerPrefsRepository(_prefsSaveKey), _boardSetup);
            _boardPresenter.Create();
            Board = _boardPresenter.Board;

            _openedItemListPresenter = new OpenedItemListPresenter(new PlayerPrefsRepository(_prefsSaveKey + OpenedItemListSaveKey), Board);
            _openedItemListPresenter.Create();

            _cellGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _cellGrid.constraintCount = Board.Width;

            var parentCanvas = _cellGrid.GetComponentInParent<Canvas>();
            var viewRect =(_cellGrid.transform as RectTransform).rect;
            float width = viewRect.width / Board.Width - _cellGrid.spacing.x * (Board.Width - 1);
            float height = viewRect.height / Board.Height - _cellGrid.spacing.y * (Board.Height - 1);
            _cellGrid.cellSize = Vector2.one * Mathf.Min(width, height);

            var itemsList = new ItemListResource().Load();
            var itemFactory = new ItemFactory(_itemTemplate, parentCanvas.transform, itemsList.Icons());
            var cellFactory = new CellFactory(_cellGrid.transform, _cellTemplate, _cellPullAnimationFactory.Create());
            BoardView = new BoardView(Board, itemFactory, cellFactory);
            BoardView.Render(BoardView.Cells[0]);

            var movePolicy = new DefaultMovePolicy(Board);
            var mergePolicy = new DefaultMergePolicy(Board, itemsList.Levels());
            var clickPolicy = new ClickPolicy(Board, _gameRules.ClickCommands(Board));

            CommandFilter = new CommandFilter(_gameRules.ClickCommands(Board).ToDictionary(item => item.Item1, item => item.Item2));
            var itemProduceInfo = new ItemProduceInfo(CommandFilter);
            var itemProduceInfoViewFactory = new ItemProduceInfoViewFactory(_itemProduceInfoView, itemProduceInfo, _openedItemListPresenter.OpenedItemList);
            SelectView[] selectedViews = _selectViews.Select(Instantiate).ToArray();
            Array.ForEach(selectedViews, view => view.Init(itemProduceInfoViewFactory));

            SelectedItem = new SelectedItem(selectedViews.ToArray());
            var moveInput = new MoveInput(BoardView, movePolicy, mergePolicy, _mergeAnimationFactory.Create());
            var clickInput = new ClickInput(BoardView, clickPolicy, _clickExceptionView, _gameRules.ClickAnimations());

            var boardInput = new BoardInput(Board, moveInput, clickInput, SelectedItem);

            foreach (var cell in BoardView.Cells)
            {
                var oldInputs = cell.GetComponents<CellInput>();

                if (oldInputs != null)
                    foreach (var oldInput in oldInputs)
                        Destroy(oldInput.gameObject);

                var input = cell.gameObject.AddComponent<CellInput>();
                input.Init(cell, boardInput, _dragAnimationFactory.Create());
            }

            Initialized = true;
        }

        private void OnDestroy()
        {
            _boardPresenter.Dispose();
            _openedItemListPresenter.Dispose();
        }
    }
}
