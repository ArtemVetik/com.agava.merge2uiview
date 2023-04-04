using Agava.Merge2.Core;

namespace Agava.Merge2UIView
{
    public interface IMergeRoot
    {
        bool Initialized { get; }
        IBoard Board { get; }
        BoardView BoardView { get; }
        SelectedItem SelectedItem { get; }
        CommandFilter CommandFilter { get; }
    }
}
