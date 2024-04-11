using Fukicycle.Tool.AppBase.Components.Dialog;
using Fukicycle.Tool.AppBase.Store;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Fukicycle.Tool.AppBase
{
    public abstract class ViewBase : ComponentBase
    {
        [Inject]
        public IStateContainer StateContainer { get; set; } = null!;

        [Inject]
        public HttpClient HttpClient { get; set; } = null!;

        [Inject]
        public ILogger<ViewBase> Logger { get; set; } = null!;

        protected async Task<T> ExecuteAsync<T>(Func<Task<T>> method, bool hasLoading = false)
        {
            try
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = true;
                }
                return await Task.Run(method);
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
                await Task.Run(method);
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

        protected async Task<T?> ExecuteWithHttpRequestAsync<T, TSource>(HttpMethod httpMethod, string endPoint, TSource? jsonBody = default, Dictionary<string, string>? headers = null, bool force = false, bool hasLoading = true)
        {
            try
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = true;
                }
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, new Uri(endPoint, UriKind.RelativeOrAbsolute));
                if (jsonBody != null)
                {
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(jsonBody), System.Text.Encoding.UTF8, "application/json");
                }
                if (headers != null && headers.Any())
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        if (httpRequestMessage.Headers.Any(a => a.Key == header.Key))
                        {
                            if (force)
                            {
                                httpRequestMessage.Headers.Remove(header.Key);
                            }
                            else
                            {
                                throw new InvalidOperationException($"This header has already added. Header name:{header.Key},Value:{header.Value}. If you want to override, you can use force option.");
                            }
                        }
                        httpRequestMessage.Headers.Add(header.Key, header.Value);
                    }
                }
                HttpResponseMessage httpResponseMessage = await HttpClient.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();
                T? item = await httpResponseMessage.Content.ReadFromJsonAsync<T>();
                if (item == null)
                {
                    string originalContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    throw new Exception($"Can not read content from HttpResponseMessage.Original content:{originalContent}");
                }
                return item;
            }
            catch (Exception ex)
            {
                StateContainer.DialogContent = new DialogContent(ex.Message, DialogType.Error);
                Logger.LogError(ex.StackTrace);
            }
            finally
            {
                if (hasLoading)
                {
                    StateContainer.IsLoading = false;
                }
            }
            return default;
        }
    }
}
