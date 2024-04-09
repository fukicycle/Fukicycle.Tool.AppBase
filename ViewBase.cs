using Fukicycle.Tool.AppBase.Components.Dialog;
using Fukicycle.Tool.AppBase.Store;
using Microsoft.AspNetCore.Components;

namespace Fukicycle.Tool.AppBase
{
    public abstract class ViewBase : ComponentBase
    {
        [Inject]
        public IStateContainer StateContainer { get; set; } = null!;

        protected delegate Task<T> RunAsync<T>();
        protected delegate Task RunAsync();
        protected delegate T Run<T>();
        protected delegate void Run();

        protected async Task<T> ExecuteAsync<T>(RunAsync<T> method, bool hasLoading = false)
        {
            try
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = true;
                }
                return await method();
            }
            catch (Exception ex)
            {
                StateContainer.DialogContent = new DialogContent(ex.Message, DialogType.Error);
            }
            finally
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = false;
                }
            }
            return default!;
        }

        protected async Task ExecuteAsync(RunAsync method, bool hasLoading = false)
        {
            try
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = true;
                }
                await method();
            }
            catch (Exception ex)
            {
                StateContainer.DialogContent = new DialogContent(ex.Message, DialogType.Error);
            }
            finally
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = false;
                }
            }
        }

        protected T Execute<T>(Run<T> method, bool hasLoading = false)
        {
            try
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = true;
                }
                return method();
            }
            catch (Exception ex)
            {
                StateContainer.DialogContent = new DialogContent(ex.Message, DialogType.Error);
            }
            finally
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = false;
                }
            }
            return default!;
        }

        protected void Execute(Run method, bool hasLoading = false)
        {
            try
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = true;
                }
                method();
            }
            catch (Exception ex)
            {
                StateContainer.DialogContent = new DialogContent(ex.Message, DialogType.Error);
            }
            finally
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = false;
                }
            }
        }
    }
}
