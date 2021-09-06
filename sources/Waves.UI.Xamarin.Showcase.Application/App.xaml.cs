using System;
using Waves.UI.Showcase.Common.Presentation.ViewModel.Pages;
using Waves.UI.Xamarin.Showcase.App.Presentation.View.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Waves.UI.Xamarin.Showcase.App
{
    public partial class App : WavesApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override async void OnStart()
        {
            try
            {
                base.OnStart();
                await NavigationService.NavigateAsync<MainPageViewModel>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}