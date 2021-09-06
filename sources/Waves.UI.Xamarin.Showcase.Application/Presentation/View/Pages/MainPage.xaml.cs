using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waves.Core.Base.Interfaces;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Attributes;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Waves.UI.Xamarin.Showcase.App.Presentation.View.Pages
{
    /// <summary>
    /// Main page.
    /// </summary>
    [WavesView(typeof(MainPageViewModel))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
    {
        /// <summary>
        /// Creates new instance of <see cref="MainPage"/>.
        /// </summary>
        /// <param name="core">Instance of <see cref="Core"/>.</param>
        /// <param name="navigationService">Instance of <see cref="IWavesNavigationService"/>.</param>
        public MainPage(
            IWavesCore core,
            IWavesNavigationService navigationService)
            : base(core, navigationService)
        {
            InitializeComponent();
        }
    }
}