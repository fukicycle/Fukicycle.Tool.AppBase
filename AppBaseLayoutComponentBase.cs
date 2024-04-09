using Fukicycle.Tool.AppBase.Store;
using Microsoft.AspNetCore.Components;

namespace Fukicycle.Tool.AppBase
{
    public class AppBaseLayoutComponentBase : LayoutComponentBase, IDisposable
    {
        [Inject]
        public IStateContainer StateContainer { get; set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            StateContainer.OnLoadStateChanged += StateHasChanged;
            StateContainer.OnDialogContentChanged += StateHasChanged;
        }
        public void Dispose()
        {
            StateContainer.OnLoadStateChanged -= StateHasChanged;
            StateContainer.OnDialogContentChanged -= StateHasChanged;
        }
    }
}
