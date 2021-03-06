using System;
using System.Threading.Tasks;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.UI.Plugins.Services.Interfaces;
using Xamarin.Forms;

namespace Waves.UI.Xamarin
{
    /// <summary>
    /// Waves Xamarin Application.
    /// </summary>
    public class WavesApplication : Application
    {
        private IWavesNavigationService _navigationService;
        private IWavesDialogService _dialogService;
        private IWavesDispatcherService _dispatcherService;

        /// <summary>
        /// Gets core.
        /// </summary>
        public IWavesCore Core { get; private set; } = new Core.Core();

        protected override async void OnStart()
        {
            base.OnStart();
            
            try
            {
                TaskScheduler.UnobservedTaskException += OnTaskSchedulerUnobservedTaskException;
                Core.MessageReceived += OnCoreMessageReceived;

                await Core.StartAsync();
                await Core.BuildContainerAsync();
                await InitializeServices();
                await InitializeGenericDictionary();
            }
            catch (Exception e)
            {
                await Core.WriteLogAsync(e, Core, true);
            }
        }

        /// <summary>
        /// Initializes services.
        /// </summary>
        private async Task InitializeServices()
        {
            _navigationService = await Core.GetInstanceAsync<IWavesNavigationService>();
            _dialogService = await Core.GetInstanceAsync<IWavesDialogService>();
            _dispatcherService = await Core.GetInstanceAsync<IWavesDispatcherService>();

            await Task.Delay(1000);
        }

        /// <summary>
        /// Adds generic styles file to app dictionaries.
        /// </summary>
        private Task InitializeGenericDictionary()
        {
            // this.AddStyle(Constants.GenericDictionaryUri);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Notifies when unhandled exception received.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void OnTaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            await Core.WriteLogAsync(e.Exception, new WavesMessageSource("Application"), true);
            e.SetObserved();
        }

        /// <summary>
        /// Callback when message from core received.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Message.</param>
        private void OnCoreMessageReceived(object sender, IWavesMessageObject e)
        {
            //// TODO: make more flexible fatal error handling.
            if (e.Type != WavesMessageType.Fatal && e.Type != WavesMessageType.Error)
            {
                return;
            }

            if (_dialogService == null && _dispatcherService == null)
            {
                return;
            }

            _dispatcherService.Invoke(() =>
            {
                _dialogService.ShowDialogAsync(e);
            });
        }
    }
}
