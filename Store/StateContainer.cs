using Fukicycle.Tool.AppBase.Components.Dialog;

namespace Fukicycle.Tool.AppBase.Store
{
    public sealed class StateContainer : IStateContainer
    {
        private DialogContent? _dialogContent = null;
        public DialogContent? DialogContent
        {
            get => _dialogContent;
            set
            {
                if (_dialogContent != value)
                {
                    _dialogContent = value;
                    if (OnDialogContentChanged == null) throw new InvalidOperationException($"You have to register event handler:{nameof(OnDialogContentChanged)}");
                    OnDialogContentChanged.Invoke();
                }
            }
        }

        public event Action? OnDialogContentChanged;

        private bool _isLoading = false;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    if (OnLoadStateChanged == null) throw new InvalidOperationException($"You have to register event handler:{nameof(OnLoadStateChanged)}");
                    OnLoadStateChanged.Invoke();
                }
            }
        }


        public event Action? OnLoadStateChanged;

    }
}
