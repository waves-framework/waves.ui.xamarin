using System.ComponentModel;
using Xamarin.Forms;

namespace Waves.UI.Xamarin.Controls
{
    /// <summary>
    /// Vector image path.
    /// </summary>
    [DefaultProperty(nameof(Value))]
    public class WavesVectorImagePath : Element
    {
        /// <summary>
        /// Defines <see cref="Value"/> property.
        /// </summary>
        public static readonly BindableProperty ValueProperty =
            BindableProperty.CreateAttached(
                nameof(Value),
                typeof(string),
                typeof(WavesVectorImagePath),
                string.Empty);
        
        /// <summary>
        /// Defines <see cref="Value"/> property.
        /// </summary>
        public static readonly BindableProperty FillProperty =
            BindableProperty.CreateAttached(
                nameof(Fill),
                typeof(Color),
                typeof(WavesVectorImagePath),
                Color.Accent);

        /// <summary>
        ///     Gets or sets value.
        /// </summary>
        [Category("Waves.UI SDK - Path")]
        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        ///     Gets or sets fill color.
        /// </summary>
        [Category("Waves.UI SDK - Path")]
        public Color Fill
        {
            get => (Color)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }
    }
}
