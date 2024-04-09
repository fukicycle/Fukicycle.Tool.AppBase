using Fukicycle.Tool.AppBase.Components.Dialog;
using Fukicycle.Tool.AppBase.Store;
using Microsoft.AspNetCore.Components;

namespace Fukicycle.Tool.AppBase
{
    public abstract class ViewBase : ComponentBase
    {
        [Inject]
        public IStateContainer StateContainer { get; set; } = null!;

        protected async Task<T> ExecuteAsync<T>(Func<Task<T>> method, bool hasLoading = false)
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

        protected async Task ExecuteAsync(Func<Task> method, bool hasLoading = false)
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

        protected T Execute<T>(Func<T> method, bool hasLoading = false)
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

        protected void Execute(Action method, bool hasLoading = false)
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
