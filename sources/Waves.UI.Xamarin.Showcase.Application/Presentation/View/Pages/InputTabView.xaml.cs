using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waves.Core.Base.Interfaces;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Attributes;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Tabs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Waves.UI.Xamarin.Showcase.App.Presentation.View.Pages
{
    [WavesView(typeof(InputTabViewModel))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputTabView
    {
        protected InputTabView(IWavesCore core, IWavesNavigationService navigationService)
            : base(core, navigationService)
        {
            InitializeComponent();
        }
    }
}