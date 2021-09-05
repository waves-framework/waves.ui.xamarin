using System;
using System.ComponentModel;
using Waves.UI.Presentation.Interfaces;
using Waves.UI.Xamarin.Helpers;
using Xamarin.Forms;

namespace Waves.UI.Xamarin.Controls
{
    /// <summary>
    /// Waves content control (view).
    /// </summary>
    public class WavesContentControl : ContentView
    {
        /// <summary>
        /// Defines <see cref="Region"/> property.
        /// </summary>
        public static readonly BindableProperty RegionProperty =
            BindableProperty.CreateAttached(
                nameof(Region),
                typeof(string),
                typeof(WavesContentControl),
                string.Empty);
        
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.CreateAttached(
                nameof(CornerRadius),
                typeof(CornerRadius),
                typeof(WavesContentControl),
                new CornerRadius(3),
                propertyChanged:OnCornerRadiusChangedCallback);

        /// <summary>
        /// Gets or sets center.
        /// </summary>
        [Category("Waves.UI SDK - Content")]
        public string Region
        {
            get => (string)GetValue(RegionProperty);
            set => SetValue(RegionProperty, value);
        }

        /// <summary>
        /// Gets or sets center.
        /// </summary>
        [Category("Waves.UI SDK - Appearance")]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Callback when corner radius changed.
        /// </summary>
        /// <param name="bindable">Bindable object.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnCornerRadiusChangedCallback(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            if (bindable is not WavesContentControl control)
            {
                return;
            }

            control.SetValue(ControlHelper.CornerRadiusProperty, newValue);
        }
    }
}