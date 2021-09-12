using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Waves.Core.Base.Interfaces;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Interfaces.View;
using Xamarin.Forms;

namespace Waves.UI.Xamarin.Controls
{
    /// <summary>
    /// Waves page.
    /// </summary>
    public class WavesNavigationPage : NavigationPage, IWavesPage<View>
    {
        private Dictionary<string, WavesContentControl> _regionContentControls;
        
        /// <summary>
        /// Creates new instance of <see cref="WavesNavigationPage"/>.
        /// </summary>
        /// <param name="core">Core.</param>
        /// <param name="navigationService">Instance of navigation service.</param>
        public WavesNavigationPage(
            IWavesCore core,
            IWavesNavigationService navigationService)
        {
            NavigationService = navigationService;
            Core = core;
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesNavigationPage"/>.
        /// </summary>
        /// <param name="core">Core.</param>
        /// <param name="navigationService">Instance of navigation service.</param>
        /// <param name="root">Root page.</param>
        public WavesNavigationPage(
            IWavesCore core,
            IWavesNavigationService navigationService,
            Page root) : base(root)
        {
            NavigationService = navigationService;
            Core = core;
        }

        /// <inheritdoc />
        public bool IsInitialized { get; }
        
        /// <inheritdoc />
        public object DataContext
        {
            get => BindingContext;
            set => BindingContext = value;
        }
        
        /// <summary>
        /// Gets core.
        /// </summary>
        protected IWavesCore Core { get; }
        
        /// <summary>
        /// Gets navigation service.
        /// </summary>
        protected IWavesNavigationService NavigationService { get; }

        protected override void OnAppearing()
        {
            // _regionContentControls = this.FindRegions(NavigationService);
            // this.InitializeTabControls(Core);
            // this.InitializeSurfaces(Core);
            
            base.OnAppearing();
        }

        /// <inheritdoc />
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
        
        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        ///     Disposes object.
        /// </summary>
        /// <param name="disposing">Set
        ///     <value>true</value>
        ///     if you need to release managed and unmanaged resources. Set
        ///     <value>false</value>
        ///     if need to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            // this.DisposeVisualTree();

            if (_regionContentControls == null)
            {
                return;
            }

            foreach (var control in _regionContentControls)
            {
                NavigationService.UnregisterContentControl(control.Key);
            }
        }

        /// <inheritdoc />
        public View Content { get; set; }
    }
}