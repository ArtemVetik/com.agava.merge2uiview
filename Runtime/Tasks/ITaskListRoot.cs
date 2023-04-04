
namespace Agava.Merge2UIView
{
    public interface ITaskListRoot
    {
        public bool Initialized { get; }
        public TaskListPresenter TaskList { get; }
    }
}
