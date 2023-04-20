using Agava.Merge2.Core;

namespace Agava.Merge2UIView
{
    public interface IMergeRoot
    {
        bool Initialized { get; }
        IBoard Board { get; }
        BoardView BoardView { get; }
        CommandFilter CommandFilter { get; }
        OpenedItemList OpenedItemList { get; }
        SelectedItem SelectedItem { get; }
        SelectedItemPanel SelectedItemPanel { get; }
    }
}
