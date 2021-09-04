using System;
using Waves.UI.Base.Enums;
using Waves.UI.Base.Interfaces;
using Xamarin.Forms;

namespace Waves.UI.Xamarin.Colors
{
    /// <summary>
    ///     Color set.
    /// </summary>
    public class ColorSet : ResourceDictionary, IThemeColorSet
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public ThemeColorSetType Type { get; set; }
    }
}