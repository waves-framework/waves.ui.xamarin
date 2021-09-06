using System;
using System.Threading.Tasks;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Xamarin.Plugins.Services;
using Xamarin.Forms;

namespace Waves.UI.Xamarin
{
    /// <summary>
    /// Waves Xamarin Application.
    /// </summary>
    public class WavesApplication : Application
    {
        private IWavesDispatcherService _dispatcherService;

        /// <summary>
        /// Gets core.
        /// </summary>
        protected IWavesCore Core { get; private set; }

        /// <summary>
        /// Gets navigation service.
        /// </summary>
        protected IWavesNavigationService NavigationService { get; private set; }
        
        /// <summary>
        /// Gets dialog service.
        /// </summary>
        protected IWavesDialogService DialogService { get; private set; }   
        
        protected override async void OnStart()
        {
            base.OnStart();
            
            try
            {
                Core = new Core.Core();
                
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
            NavigationService = await Core.GetInstanceAsync<IWavesNavigationService>();
            DialogService = await Core.GetInstanceAsync<IWavesDialogService>();
            _dispatcherService = await Core.GetInstanceAsync<IWavesDispatcherService>();

            if (NavigationService is WavesNavigationService xamarinNavigationService)
            {
                xamarinNavigationService.AttachApplication(this);
            }

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

            if (DialogService == null && _dispatcherService == null)
            {
                return;
            }

            _dispatcherService.Invoke(() =>
            {
                DialogService.ShowDialogAsync(e);
            });
        }
    }
}
