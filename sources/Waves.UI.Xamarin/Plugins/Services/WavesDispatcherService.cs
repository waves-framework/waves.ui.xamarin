using System;
using System.Threading.Tasks;
using Waves.Core.Base;
using Waves.Core.Base.Attributes;
using Waves.UI.Plugins.Services.Interfaces;
using Xamarin.Forms;

namespace Waves.UI.Xamarin.Plugins.Services
{
    /// <summary>
    /// WPF Dispatcher service.
    /// </summary>
    [WavesService(typeof(IWavesDispatcherService))]
    public class WavesDispatcherService : WavesService, IWavesDispatcherService
    {
        /// <inheritdoc />
        public void Invoke(Action action)
        {
            Application.Current.Dispatcher.BeginInvokeOnMainThread(action);
        }

        /// <inheritdoc />
        public Task InvokeAsync(Action action)
        {
             Application.Current.Dispatcher.BeginInvokeOnMainThread(action);
             return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "Xamarin Dispatcher Service";
        }
    }
}
