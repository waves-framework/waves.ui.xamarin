using Waves.UI.Xamarin.Controls.Enums;
using Xamarin.Forms;

namespace Waves.UI.Xamarin.Helpers
{
    /// <summary>
    ///     Control's dependency property helper.
    /// </summary>
    public static class ControlHelper
    {
        /// <summary>
        ///     Gets or sets control's "Corner Radius" property.
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.CreateAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(ControlHelper),
                new CornerRadius(0, 0, 0, 0));

        /// <summary>
        ///     Gets or sets control's "Region" property.
        /// </summary>
        public static readonly BindableProperty RegionProperty =
            BindableProperty.CreateAttached(
                "Region",
                typeof(string),
                typeof(ControlHelper),
                string.Empty);

        /// <summary>
        /// Defines "IconSource" property.
        /// </summary>
        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.CreateAttached(
            "IconSource",
            typeof(object),
            typeof(ControlHelper),
            null);

        /// <summary>
        /// Defines "IconSourceDirectory" property.
        /// </summary>
        public static readonly BindableProperty IconSourceDirectoryProperty =
            BindableProperty.CreateAttached(
            "IconSourceDirectory",
            typeof(string),
            typeof(ControlHelper),
            null);

        /// <summary>
        /// Defines "IconSourceAssembly" property.
        /// </summary>
        public static readonly BindableProperty IconSourceAssemblyProperty =
            BindableProperty.CreateAttached(
            "IconSourceAssembly",
            typeof(string),
            typeof(ControlHelper),
            string.Empty);

        /// <summary>
        /// Defines "IconSourceType" property.
        /// </summary>
        public static readonly BindableProperty IconSourceTypeProperty =
            BindableProperty.CreateAttached(
            "IconSourceType",
            typeof(WavesVectorImageSourceType),
            typeof(ControlHelper),
            WavesVectorImageSourceType.Unknown);

        /// <summary>
        ///     Gets control's "Corner Radius".
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Corner radius.</returns>
        public static CornerRadius GetCornerRadius(BindableObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        ///     Sets control's "Corner Radius" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Corner radius.</param>
        public static void SetCornerRadius(BindableObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets control's "Region" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>Region value.</returns>
        public static string GetRegion(BindableObject obj)
        {
            return (string)obj.GetValue(RegionProperty);
        }

        /// <summary>
        /// Sets control's "Region" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">Region value.</param>
        public static void SetRegion(BindableObject obj, string value)
        {
            obj.SetValue(RegionProperty, value);
        }

        /// <summary>
        /// Gets control's "IconSource" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>IconSource value.</returns>
        public static object GetIconSource(BindableObject obj)
        {
            return obj.GetValue(IconSourceProperty);
        }

        /// <summary>
        /// Sets control's "IconSource" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">IconSource value.</param>
        public static void SetIconSource(BindableObject obj, object value)
        {
            obj.SetValue(IconSourceProperty, value);
        }

        /// <summary>
        /// Gets control's "IconSourceDirectory" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>IconSourceDirectory value.</returns>
        public static string GetIconSourceDirectory(BindableObject obj)
        {
            return (string)obj.GetValue(IconSourceDirectoryProperty);
        }

        /// <summary>
        /// Sets control's "IconSourceDirectory" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">IconSourceDirectory value.</param>
        public static void SetIconSourceDirectory(BindableObject obj, string value)
        {
            obj.SetValue(IconSourceDirectoryProperty, value);
        }

        /// <summary>
        /// Gets control's "IconSourceAssembly" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>IconSourceAssembly value.</returns>
        public static string GetIconSourceAssembly(BindableObject obj)
        {
            return (string)obj.GetValue(IconSourceAssemblyProperty);
        }

        /// <summary>
        /// Sets control's "IconSourceAssembly" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">IconSourceAssembly value.</param>
        public static void SetIconSourceAssembly(BindableObject obj, string value)
        {
            obj.SetValue(IconSourceAssemblyProperty, value);
        }

        /// <summary>
        /// Gets control's "IconSourceType" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <returns>IconSourceType value.</returns>
        public static WavesVectorImageSourceType GetIconSourceType(BindableObject obj)
        {
            return (WavesVectorImageSourceType)obj.GetValue(IconSourceTypeProperty);
        }

        /// <summary>
        /// Sets control's "IconSourceType" property.
        /// </summary>
        /// <param name="obj">Dependency object.</param>
        /// <param name="value">IconSourceType value.</param>
        public static void SetIconSourceType(BindableObject obj, WavesVectorImageSourceType value)
        {
            obj.SetValue(IconSourceTypeProperty, value);
        }
    }
}
