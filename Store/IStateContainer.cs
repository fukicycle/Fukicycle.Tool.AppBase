using Fukicycle.Tool.AppBase.Components.Dialog;
namespace Fukicycle.Tool.AppBase.Store
{
    public interface IStateContainer
    {
        event Action OnDialogContentChanged;
        public DialogContent? DialogContent { get; set; }

        event Action OnLoadStateChanged;
        public bool IsLoading { get; set; }
    }
}
