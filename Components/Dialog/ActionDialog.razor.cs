namespace Fukicycle.Tool.AppBase.Components.Dialog
{
    public partial class ActionDialog
    {
        private bool _isShow = false;
        private TaskCompletionSource<bool>? _taskCompletionSource;

        private string _header = "";
        private string _content = "";
        private DialogType _dialogType = DialogType.Information;

        private void CloseButtonOnClick()
        {
            _isShow = false;
            if (_taskCompletionSource == null)
            {
                throw new NullReferenceException("Unexpected value.");
            }
            _taskCompletionSource.SetResult(false);
            _taskCompletionSource = null;
        }

        private void OkButtonOnClick()
        {
            _isShow = false;
            if (_taskCompletionSource == null)
            {
                throw new NullReferenceException("Unexpected value.");
            }
            _taskCompletionSource.SetResult(true);
            _taskCompletionSource = null;
        }
        public Task<bool> ShowAsync(string content)
        {
            return ShowAsync(_dialogType.ToString(), content, _dialogType);
        }

        public Task<bool> ShowAsync(string content, DialogType dialogType)
        {
            return ShowAsync(_dialogType.ToString(), content, dialogType);
        }

        public Task<bool> ShowAsync(string header, string content)
        {
            return ShowAsync(header, content, _dialogType);
        }

        public Task<bool> ShowAsync(string header, string content, DialogType dialogType)
        {
            _isShow = true;
            _taskCompletionSource = new TaskCompletionSource<bool>();
            _header = header;
            _content = content;
            _dialogType = dialogType;
            StateHasChanged();
            return _taskCompletionSource.Task;
        }
    }
}
