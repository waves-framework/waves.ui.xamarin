using System.Collections.Generic;
using System.Threading.Tasks;
using Waves.Core.Base.Attributes;
using Waves.Core.Base.Interfaces;
using Waves.Core.Plugins.Services.EventArgs;
using Waves.UI.Plugins.Services;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Interfaces;
using Xamarin.Forms;

namespace Waves.UI.Xamarin.Plugins.Services
{
    /// <summary>
    /// Navigation service.
    /// </summary>
    [WavesService(typeof(IWavesNavigationService))]
    public class WavesNavigationService : WavesNavigationServiceBase
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesNavigationService"/>.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        public WavesNavigationService(
            IWavesCore core)
            :base(core)
        {
        }
        
        /// <summary>
        /// Gets dictionary of Content controls keyed by region.
        /// </summary>
        private Dictionary<string, ContentView> ContentViews { get; set; }

        /// <inheritdoc />
        public override Task InitializeAsync()
        {
            ContentViews = new Dictionary<string, ContentView>();
            return base.InitializeAsync();
        }

        /// <inheritdoc />
        public override void RegisterContentControl(string region, object ContentView)
        {
            if (ContentView is not ContentView control)
            {
                return;
            }

            AddContentView(region, control);

            if (!PendingActions.ContainsKey(region))
            {
                return;
            }

            PendingActions[region].Invoke();
            PendingActions.Remove(region);
        }

        /// <inheritdoc />
        public override void UnregisterContentControl(string region)
        {
            if (!ContentViews.ContainsKey(region))
            {
                return;
            }

            ContentViews.Remove(region);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "AvaloniaUI Navigation Service";
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            base.Dispose();
            
            if (!disposing)
            {
                return;
            }

            ContentViews.Clear();
        }

        /// <summary>
        /// Navigates to windows.
        /// </summary>
        /// <param name="view">Window view.</param>
        /// <param name="viewModel">ViewModel.</param>
        protected override async Task InitializeWindowAsync(IWavesWindow view, IWavesViewModel viewModel)
        {
            var region = await InitializeComponents(view, viewModel);
            var contentView = view as ContentView;
            if (contentView == null)
            {
                return;
            }
            
            void Action()
            {
                view.Show();
                RegisterView(contentView);
            }
            
            Application.Current.Dispatcher.BeginInvokeOnMainThread(Action);

            AddContentView(region, contentView);
        }

        /// <summary>
        /// Navigates to page.
        /// </summary>
        /// <param name="view">Page view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        protected override async Task InitializePageAsync(IWavesPage view, IWavesViewModel viewModel, bool addToHistory = true)
        {
            var region = await InitializeComponents(view, viewModel);

            void Action()
            {
                AddToHistoryStack(region, viewModel, addToHistory);
                var contentView = ContentViews[region];
                if (contentView is IWavesWindow window)
                {
                    // window.FrontLayerContent = null;
                }

                if (contentView.Content != null && contentView.Content.GetType() == view.GetType())
                {
                    return;
                }

                FadeOutUiElement(contentView);
                UnregisterView(contentView);
                ContentViews[region].Content = (View)view;
                FadeInUiElement(contentView);
                RegisterView(contentView);

                OnGoBackChanged(
                    new GoBackNavigationEventArgs(
                        Histories[region].Count > 1,
                        ContentViews[region]));
            }

            if (!ContentViews.ContainsKey(region))
            {
                PendingActions.Add(region, Action);
            }
            else
            {
                Application.Current.Dispatcher.BeginInvokeOnMainThread(Action);
            }
        }

        /// <summary>
        /// Navigates to user control.
        /// </summary>
        /// <param name="view">User control view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        protected override async Task InitializeUserControlAsync(IWavesUserControl view, IWavesViewModel viewModel, bool addToHistory = true)
        {
            var region = await InitializeComponents(view, viewModel);
        
            void Action()
            {
                AddToHistoryStack(region, viewModel, addToHistory);
                var contentView = ContentViews[region];
                FadeOutUiElement(contentView);
                UnregisterView(contentView);
                view.Opacity = 0;
                ContentViews[region].Content = (View)view;
                FadeInUiElement(contentView);
                RegisterView(contentView);
            }
        
            if (!ContentViews.ContainsKey(region))
            {
                PendingActions.Add(region, Action);
            }
            else
            {
                Application.Current.Dispatcher.BeginInvokeOnMainThread(Action);
            }
        }
        
        /// <summary>
        /// Navigates to dialog.
        /// </summary>
        /// <param name="view">Dialog view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        protected override async Task InitializeDialogAsync(IWavesDialog view, IWavesDialogViewModel viewModel, bool addToHistory = true)
        {
            var region = await InitializeComponents(view, viewModel);
            var styledElement = view as VisualElement;
            if (styledElement == null)
            {
                return;
            }
            
            void Action()
            {
                AddToHistoryStack(region, viewModel, addToHistory);
                DialogSessions.Add(viewModel);
                CheckDialogs();
                var contentView = ContentViews[region];
                if (contentView is IWavesWindow window)
                {
                    UnregisterView(contentView);
                    // window.FrontLayerContent = styledElement;
                    RegisterView(contentView);
                }
                else
                {
                    // TODO: what if another content control?
                }
            }
        
            if (!ContentViews.ContainsKey(region))
            {
                PendingActions.Add(region, Action);
            }
            else
            {
                Application.Current.Dispatcher.BeginInvokeOnMainThread(Action);
            }
        }

        /// <summary>
        /// Checks dialogs.
        /// </summary>
        private void CheckDialogs()
        {
            if (DialogSessions.Count > 0)
            {
                OnDialogsShown();
            }
            else
            {
                OnDialogsHidden();
            }
        }

        /// <summary>
        /// Adds new window to content control dictionary.
        /// </summary>
        /// <param name="region">Region.</param>
        /// <param name="view">Content control.</param>
        private void AddContentView(string region, ContentView view)
        {
            if (!ContentViews.ContainsKey(region))
            {
                ContentViews.Add(region, view);
            }
            else
            {
                // rewrite if controls with same region are not equal.
                if (ContentViews[region].Equals(view))
                {
                    return;
                }

                ContentViews[region] = view;
            }
        }

        /// <summary>
        /// Animates fade in for <see cref="StyledElement"/> is current <see cref="ContentView"/>.
        /// </summary>
        /// <param name="control">Instance of <see cref="ContentView"/>.</param>
        private void FadeInUiElement(ContentView control)
        {
            if (control.Content is not VisualElement element)
            {
                return;
            }

            // element.AnimateOpacity(0, 1, 100);
        }

        /// <summary>
        /// Animates fade out for <see cref="StyledElement"/> is current <see cref="ContentView"/>.
        /// </summary>
        /// <param name="control">Instance of <see cref="ContentView"/>.</param>
        private void FadeOutUiElement(ContentView control)
        {
            if (control.Content is not VisualElement element)
            {
                return;
            }

            // element.AnimateOpacity(1, 0, 100);
        }

        /// <summary>
        /// Invokes <see cref="IWavesViewModel.ViewAppeared"/> for <see cref="StyledElement"/> is current <see cref="ContentView"/>.
        /// </summary>
        /// <param name="control">Instance of <see cref="ContentView"/>.</param>
        private void RegisterView(ContentView control)
        {
            if (control.Content is not VisualElement element)
            {
                return;
            }

            // TODO: regions?
            // var controls = element.FindRegions(this);

            if (element.BindingContext is IWavesViewModel viewModel)
            {
                viewModel.ViewAppeared();
            }
        }

        /// <summary>
        /// Invokes <see cref="IWavesViewModel.ViewDisappeared"/> for <see cref="StyledElement"/> is current <see cref="ContentView"/>.
        /// </summary>
        /// <param name="control">Instance of <see cref="ContentView"/>.</param>
        private void UnregisterView(ContentView control)
        {
            if (control.Content is not VisualElement element)
            {
                return;
            }
            
            
            if (element.BindingContext is IWavesViewModel viewModel)
            {
                viewModel.ViewDisappeared();
            }

            if (element is IWavesView view)
            {
                view.Dispose();
            }
        }
    }
}
