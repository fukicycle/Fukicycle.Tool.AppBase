namespace Fukicycle.Tool.AppBase.Components.Dialog
{
    public sealed class DialogContent
    {
        public DialogContent(string message, DialogType dialogType)
        {
            Message = message;
            DialogType = dialogType;
        }
        public string Message { get; }
        public DialogType DialogType { get; }
    }
}
